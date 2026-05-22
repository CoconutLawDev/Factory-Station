using System;
using Content.Server.Atmos.EntitySystems;
using Content.Server.Explosion.EntitySystems;
using Content.Server.Damage.Systems;
using Content.Server.FactoryStation.Components;
using Content.Shared.Damage;
using Content.Shared.Lathe;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Log;
using Robust.Shared.Random;
using Content.Shared.Damage.Components;
using Content.Shared.Damage.Systems;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class IndustrialHeatSystem : EntitySystem
{
    [Dependency] private SharedAudioSystem _audio = default!;
    [Dependency] private DamageableSystem _damageable = default!;
    [Dependency] private ExplosionSystem _explosion = default!;
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private AtmosphereSystem _atmosphere = default!; // <-- ДОБАВЛЕНО

    private readonly ISawmill _sawmill = Logger.GetSawmill("factory.heat");

    private float _updateAccumulator;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<FactoryIndustrialHeatComponent, LatheStartPrintingEvent>(OnLatheStarted);
    }

    private void OnLatheStarted(
        EntityUid uid,
        FactoryIndustrialHeatComponent component,
        ref LatheStartPrintingEvent args)
    {
        component.CurrentHeat += 35f;
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        _updateAccumulator += frameTime;
        if (_updateAccumulator < 1f)
            return;

        _updateAccumulator = 0f;

        var query = EntityQueryEnumerator<FactoryIndustrialHeatComponent, LatheComponent>();

        while (query.MoveNext(out var uid, out var heat, out var lathe))
        {
            var running = lathe.CurrentRecipe != null;

            // 1. Базовый нагрев/охлаждение
            if (running)
            {
                heat.CurrentHeat += heat.HeatPerSecond;
                EnsureRunningSound(uid, heat);
            }
            else
            {
                heat.CurrentHeat -= heat.CooldownPerSecond;
                StopRunningSound(heat);
            }

            // 2. Применяем атмосферное охлаждение (новое!)
            if (heat.AmbientCoolingEnabled)
                ApplyAmbientCooling(uid, heat, running);

            // 3. Ограничиваем температуру
            heat.CurrentHeat = Math.Clamp(heat.CurrentHeat, 20f, heat.MaxHeat);

            // 4. Определение состояния
            var state = heat.CurrentHeat >= heat.CriticalThreshold ? OverheatState.Critical
                : heat.CurrentHeat >= heat.DangerThreshold ? OverheatState.Warning
                : OverheatState.Normal;

            // 5. Обработка критического состояния
            if (state == OverheatState.Critical)
            {
                ProcessCriticalOverheat(uid, heat);
            }

            // 6. Дым (одиночный) – облака создаются в FactorySmokeSystem
            if (heat.ProducingSmoke && heat.CurrentHeat >= heat.SmokeThreshold)
            {
                heat.SmokeAccumulator++;
                if (heat.SmokeAccumulator >= heat.SmokeInterval)
                {
                    heat.SmokeAccumulator = 0f;
                    Spawn("FactoryHeavySmoke", Transform(uid).Coordinates);
                }
            }
        }
    }

    /// <summary>
    /// Охлаждение или нагрев станка в зависимости от температуры окружающего воздуха.
    /// </summary>
    private void ApplyAmbientCooling(EntityUid uid, FactoryIndustrialHeatComponent heat, bool running)
    {
        var mixture = _atmosphere.GetContainingMixture(uid, true);
        float ambientTemp;

        if (mixture != null)
        {
            ambientTemp = mixture.Temperature;
        }
        else if (heat.RequireAtmosphereForCooling)
        {
            // Вакуум и флаг требует атмосферу — ничего не делаем
            return;
        }
        else
        {
            // Вакуум, но охлаждение разрешено (радиационное) — считаем космический холод
            ambientTemp = 2.7f;
        }

        // Если окружение холоднее комнатной температуры – охлаждаем станок
        if (ambientTemp < heat.RoomTemperature && ambientTemp > heat.MinAmbientTemperature)
        {
            float tempDiff = heat.CurrentHeat - ambientTemp; // >0
            if (tempDiff > 0)
            {
                float cooling = tempDiff * heat.AmbientCoolingCoefficient;
                heat.CurrentHeat -= cooling;
            }
        }
        // Если окружение горячее станка – ускоряем нагрев
        else if (ambientTemp > heat.CurrentHeat)
        {
            float tempDiff = ambientTemp - heat.CurrentHeat;
            heat.CurrentHeat += tempDiff * heat.AmbientCoolingCoefficient;
        }

        // Не даём температуре упасть ниже 20°C (или ниже комнатной при работе)
        if (running)
            heat.CurrentHeat = Math.Max(heat.CurrentHeat, 20f);
    }

    private void EnsureRunningSound(EntityUid uid, FactoryIndustrialHeatComponent component)
    {
        if (component.RunningSound == null) return;
        if (component.AudioStream != null) return;

        var stream = _audio.PlayPvs(
            component.RunningSound,
            uid,
            AudioParams.Default
                .WithLoop(true)
                .WithVolume(-4f));

        if (stream == null)
        {
            _sawmill.Warning($"Failed to create sound stream for entity {uid}");
            return;
        }

        component.AudioStream = stream.Value.Entity;
    }

    private void StopRunningSound(FactoryIndustrialHeatComponent component)
    {
        if (component.AudioStream == null) return;
        _audio.Stop(component.AudioStream.Value);
        component.AudioStream = null;
    }

    private void ProcessCriticalOverheat(EntityUid uid, FactoryIndustrialHeatComponent heat)
    {
        // Повреждение станка
        if (TryComp<DamageableComponent>(uid, out var damageable))
        {
            var damageSpec = new DamageSpecifier
            {
                DamageDict = { ["Heat"] = heat.DamagePerSecondCritical }
            };
            _damageable.TryChangeDamage(uid, damageSpec, interruptsDoAfters: false);
        }

        // Шанс взрыва
        if (_random.Prob(heat.ExplosionChance))
        {
            _explosion.QueueExplosion(
                uid,
                "Default",
                heat.ExplosionIntensity,
                heat.ExplosionSlope,
                heat.ExplosionMaxTileIntensity,
                user: null,
                addLog: true);

            // Сброс температуры после взрыва
            heat.CurrentHeat = heat.MaxHeat * 0.6f;
        }
    }

    private enum OverheatState
    {
        Normal,
        Warning,
        Critical
    }
}
