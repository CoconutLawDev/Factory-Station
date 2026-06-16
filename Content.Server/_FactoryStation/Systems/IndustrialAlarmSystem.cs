using Content.Server.FactoryStation.Components;
using Content.Shared.Lathe;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Server.GameObjects; // для SharedPointLightSystem

namespace Content.Server.FactoryStation.Systems;

public sealed partial class IndustrialAlarmSystem : EntitySystem
{
    [Dependency] private SharedAudioSystem _audio = default!;
    [Dependency] private SharedPointLightSystem _pointLight = default!;

    private float _updateAccumulator;

    public override void Update(float frameTime)
    {
        _updateAccumulator += frameTime;
        if (_updateAccumulator < 1f)
            return;

        _updateAccumulator = 0f;

        var query = EntityQueryEnumerator<FactoryIndustrialHeatComponent, LatheComponent>();

        while (query.MoveNext(out var uid, out var heat, out var lathe))
        {
            bool critical = heat.CurrentHeat >= heat.CriticalThreshold;

            if (critical)
            {
                EnsureAlarm(uid, heat);
            }
            else
            {
                StopAlarm(heat, uid);
            }
        }
    }

    private void EnsureAlarm(EntityUid uid, FactoryIndustrialHeatComponent comp)
    {
        // Звук тревоги
        if (comp.AlarmSound != null && comp.AlarmStream == null)
        {
            var stream = _audio.PlayPvs(comp.AlarmSound, uid,
                AudioParams.Default.WithLoop(true).WithVolume(-2f));
            if (stream != null)
                comp.AlarmStream = stream.Value.Entity;
        }

        // Красный мигающий свет
        if (TryComp<PointLightComponent>(uid, out var light))
        {
            _pointLight.SetEnabled(uid, true, light);
            _pointLight.SetColor(uid, Color.Red, light);
            _pointLight.SetRadius(uid, 3f, light);
            _pointLight.SetEnergy(uid, 2f, light);
        }
    }

    private void StopAlarm(FactoryIndustrialHeatComponent comp, EntityUid uid)
    {
        if (comp.AlarmStream != null)
        {
            _audio.Stop(comp.AlarmStream.Value);
            comp.AlarmStream = null;
        }

        // Возвращаем свет в исходное состояние (можно погасить или сделать обычный)
        if (TryComp<PointLightComponent>(uid, out var light))
        {
            _pointLight.SetEnabled(uid, false, light);
            // Если у станка есть стандартный цвет/радиус, восстановите здесь.
        }
    }
}
