using System;
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

            // 1. Изменение температуры
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

            heat.CurrentHeat = Math.Clamp(heat.CurrentHeat, 20f, heat.MaxHeat);

            // 2. Определение состояния
            var state = heat.CurrentHeat >= heat.CriticalThreshold ? OverheatState.Critical
                : heat.CurrentHeat >= heat.DangerThreshold ? OverheatState.Warning
                : OverheatState.Normal;

            // 3. Обработка критического состояния
            if (state == OverheatState.Critical)
            {
                ProcessCriticalOverheat(uid, heat);
            }

            // 4. Генерация одиночного дыма (облако создаётся в FactorySmokeSystem)
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
            // Урон теплом – подставьте нужный тип, если "Heat" нет, замените на "Blunt" или "Slash"
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

            // Сброс температуры после взрыва, чтобы не взрывалось каждый тик
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
