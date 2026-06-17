using Content.Server.Atmos.EntitySystems;
using Content.Server.FactoryStation.Components;
using Content.Shared.Atmos;
using Content.Shared.Lathe;
using Robust.Shared.Map.Components;
using Robust.Shared.GameObjects;

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

            // Нормируем нагрев на объём помещения: в маленьких комнатах греется быстрее, но не выше 1500K
            if (mixture.Temperature < 1500f)
            {
                // Чем больше объём смеси, тем меньше нагрев на градус
                var volumeFactor = Math.Clamp(mixture.Volume, 1f, 1000f);
                var heatToAdd = heat.AtmosHeatPerSecond * 1000f / volumeFactor;
                _atmosphere.AddHeat(mixture, heatToAdd);
            }

            if (heat.CurrentHeat >= heat.DangerThreshold)
            {
                mixture.AdjustMoles(Gas.CarbonDioxide, heat.CO2PerSecond);
            }

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

                    var tileMixture = _atmosphere.GetTileMixture(transform.GridUid.Value, null, tile, true);
                    if (tileMixture != null && tileMixture.GetMoles(Gas.Oxygen) > 0)
                    {
                        _atmosphere.HotspotExpose(
                            transform.GridUid.Value,
                            tile,
                            1000f,
                            50f,
                            soh: true);
                    }
                }
            }
        }
    }
}
