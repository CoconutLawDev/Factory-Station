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
            // Станок не работает
            if (lathe.CurrentRecipe == null)
                continue;

            // Недостаточно температуры
            if (heat.CurrentHeat < heat.SmokeThreshold)
                continue;

            SpawnSmokeCloud(uid, heat);
        }
    }

    private void SpawnSmokeCloud(
        EntityUid uid,
        FactoryIndustrialHeatComponent heat)
    {
        var origin = Transform(uid).Coordinates;

        // Центральный дым
        Spawn("FactoryHeavySmoke", origin);

        var radius = (int)heat.SmokeRadius;

        for (var x = -radius; x <= radius; x++)
        {
            for (var y = -radius; y <= radius; y++)
            {
                // Пропускаем центр
                if (x == 0 && y == 0)
                    continue;

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
