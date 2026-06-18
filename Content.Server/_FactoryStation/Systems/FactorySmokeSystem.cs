using System.Numerics;
using Content.Server.FactoryStation.Components;
using Content.Shared.Lathe;
using Robust.Shared.Map;

namespace Content.Server.FactoryStation.Systems;

public sealed class FactorySmokeSystem : EntitySystem
{
    private float _accumulator;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        _accumulator += frameTime;

        if (_accumulator < 2f)
            return;

        _accumulator = 0f;

        var query = EntityQueryEnumerator<
            FactoryIndustrialHeatComponent,
            LatheComponent>();

        while (query.MoveNext(out var uid, out var heat, out var lathe))
        {
            if (lathe.CurrentRecipe == null)
            {
                heat.SmokeActiveTime = 0f;
                heat.CurrentSmokeRadius = heat.MinSmokeRadius;
                continue;
            }

            if (heat.CurrentHeat < heat.SmokeThreshold)
            {
                heat.SmokeActiveTime = 0f;
                heat.CurrentSmokeRadius = heat.MinSmokeRadius;
                continue;
            }

            heat.SmokeActiveTime += 2f;

            heat.CurrentSmokeRadius = heat.MinSmokeRadius +
                (heat.SmokeActiveTime / heat.SmokeSpreadInterval) * heat.SmokeExpansionRate;

            heat.CurrentSmokeRadius = Math.Min(heat.CurrentSmokeRadius, heat.SmokeRadius);

            SpawnSmokeCloud(uid, heat);
        }
    }

    private void SpawnSmokeCloud(
        EntityUid uid,
        FactoryIndustrialHeatComponent heat)
    {
        var origin = Transform(uid).Coordinates;

        var radius = (int)MathF.Ceiling(heat.CurrentSmokeRadius);

        // Спавним все тайлы от центра до текущего радиуса
        for (var x = -radius; x <= radius; x++)
        {
            for (var y = -radius; y <= radius; y++)
            {
                // Круг вместо квадрата
                if (x * x + y * y > radius * radius)
                    continue;

                var offset = new Vector2(x, y);
                var coords = origin.Offset(offset);

                Spawn("FactoryHeavySmoke", coords);
            }
        }
    }
}
