using Content.Server.FactoryStation.Components;
using Content.Shared.Lathe;
using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Map.Components; // MapGridComponent
using Robust.Shared.Random;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class IndustrialSpillageSystem : EntitySystem
{
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private SharedMapSystem _mapSystem = default!;

    private float _updateAccumulator;

    public override void Update(float frameTime)
    {
        _updateAccumulator += frameTime;
        if (_updateAccumulator < 1f)
            return;

        _updateAccumulator = 0f;

        var query = EntityQueryEnumerator<FactoryIndustrialHeatComponent, LatheComponent, TransformComponent>();

        while (query.MoveNext(out var uid, out var heat, out var lathe, out var xform))
        {
            if (lathe.CurrentRecipe == null)
                continue;

            heat.SpillageAccumulator += 1f;
            if (heat.SpillageAccumulator < heat.SpillageInterval)
                continue;

            heat.SpillageAccumulator = 0f;

            if (_random.Prob(heat.SpillageChance))
            {
                SpawnSpillage(heat, xform);
            }
        }
    }

    private void SpawnSpillage(FactoryIndustrialHeatComponent heat, TransformComponent xform)
    {
        var gridUid = xform.GridUid;
        if (gridUid == null || !TryComp<MapGridComponent>(gridUid, out var grid))
            return;

        // Позиция станка в тайлах
        var tilePos = _mapSystem.TileIndicesFor(gridUid.Value, grid, xform.Coordinates);

        // Собираем 8 соседних позиций
        var neighborOffsets = new Vector2i[]
        {
            new(-1, -1), new(0, -1), new(1, -1),
            new(-1,  0),              new(1,  0),
            new(-1,  1), new(0,  1), new(1,  1)
        };

        var validTiles = new List<Vector2i>();

        foreach (var offset in neighborOffsets)
        {
            var neighborTile = tilePos + offset;

            // Проверяем, что тайл не занят чем-то "твёрдым"
            if (!IsTileBlocked(gridUid.Value, grid, neighborTile))
                validTiles.Add(neighborTile);
        }

        // Если нет свободных тайлов, не спавним (или, как fallback, спавним под станок)
        if (validTiles.Count == 0)
            return;

        // Выбираем случайный свободный
        var chosenTile = _random.Pick(validTiles);

        // Координаты центра тайла
        var spawnCoords = _mapSystem.GridTileToLocal(gridUid.Value, grid, chosenTile);

        // Спавн лужи
        Spawn(heat.SpillagePrototype, spawnCoords);

        // Искры
        if (heat.EmitsSparks)
        {
            Spawn("EffectSparks", spawnCoords);
        }
    }

    private bool IsTileBlocked(EntityUid gridUid, MapGridComponent grid, Vector2i tilePos)
    {
        // Проверяем anchored entities на тайле – если есть хотя бы одна, считаем занятым
        var anchored = _mapSystem.GetAnchoredEntitiesEnumerator(gridUid, grid, tilePos);
        return anchored.MoveNext(out _);
    }
}
