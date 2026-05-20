using Content.Server.Atmos.EntitySystems;
using Content.Server.FactoryStation.Components;
using Content.Shared.Atmos;
using Content.Shared.Lathe;
using Robust.Shared.Map.Components; // MapGridComponent
using Robust.Shared.GameObjects;    // SharedMapSystem

namespace Content.Server.FactoryStation.Systems;

public sealed partial class FactoryAtmosHeatSystem : EntitySystem
{
    [Dependency] private AtmosphereSystem _atmosphere = default!;
    [Dependency] private SharedMapSystem _mapSystem = default!;

    private float _accumulator;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        _accumulator += frameTime;
        if (_accumulator < 1f)
            return;

        _accumulator = 0f;

        var query = EntityQueryEnumerator<
            FactoryIndustrialHeatComponent,
            LatheComponent>();

        while (query.MoveNext(out var uid, out var heat, out var lathe))
        {
            if (lathe.CurrentRecipe == null)
                continue;

            var mixture = _atmosphere.GetContainingMixture(uid, true);
            if (mixture == null)
                continue;

            // 1. Нагрев атмосферы
            _atmosphere.AddHeat(mixture, heat.AtmosHeatPerSecond * 1000);

            // 2. Выброс CO₂
            if (heat.CurrentHeat >= heat.DangerThreshold)
            {
                mixture.AdjustMoles(Gas.CarbonDioxide, heat.CO2PerSecond);
            }

            // 3. Принудительный поджог при критическом перегреве
            if (heat.CurrentHeat >= heat.CriticalThreshold)
            {
                var transform = Transform(uid);
                if (transform.GridUid != null &&
                    TryComp<MapGridComponent>(transform.GridUid.Value, out var grid))
                {
                    var tile = _mapSystem.TileIndicesFor(
                        transform.GridUid.Value,
                        grid,
                        transform.Coordinates);

                    _atmosphere.HotspotExpose(
                        transform.GridUid.Value,
                        tile,
                        1000f,   // температура источника
                        50f,     // объём для воспламенения
                        soh: true);
                }
            }
        }
    }
}
