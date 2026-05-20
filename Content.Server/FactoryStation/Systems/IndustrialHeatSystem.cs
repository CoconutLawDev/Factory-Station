using System;
using Content.Server.FactoryStation.Components;
using Content.Shared.Lathe;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Log;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class IndustrialHeatSystem : EntitySystem
{
    [Dependency] private SharedAudioSystem _audio = default!;

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

            if (running)
            {
                UpdateRunningState(uid, heat);
            }
            else
            {
                UpdateIdleState(heat);
            }

            heat.CurrentHeat = Math.Clamp(
                heat.CurrentHeat,
                20f,
                heat.MaxHeat);

            UpdateSmoke(uid, heat);
        }
    }

    private void UpdateRunningState(
        EntityUid uid,
        FactoryIndustrialHeatComponent component)
    {
        component.CurrentHeat += component.HeatPerSecond;

        if (component.RunningSound == null)
            return;

        // Уже играет
        if (component.AudioStream != null)
            return;

        _sawmill.Info($"Starting furnace sound for entity {uid}");

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

        _sawmill.Info($"Created audio stream entity {component.AudioStream}");
    }

    private void UpdateIdleState(
        FactoryIndustrialHeatComponent component)
    {
        component.CurrentHeat -= component.CooldownPerSecond;

        if (component.AudioStream == null)
            return;

        _audio.Stop(component.AudioStream.Value);

        component.AudioStream = null;
    }

    private void UpdateSmoke(
        EntityUid uid,
        FactoryIndustrialHeatComponent component)
    {
        if (!component.ProducingSmoke)
            return;

        if (component.CurrentHeat < component.SmokeThreshold)
            return;

        component.SmokeAccumulator += 1f;

        if (component.SmokeAccumulator < component.SmokeInterval)
            return;

        component.SmokeAccumulator = 0f;

        Spawn(
            "FactoryHeavySmoke",
            Transform(uid).Coordinates);
    }
}
