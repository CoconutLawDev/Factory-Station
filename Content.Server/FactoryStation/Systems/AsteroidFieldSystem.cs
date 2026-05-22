using System.Linq;
using System.Numerics;
using Content.Server.FactoryStation.Components;
using Content.Server.GameTicking.Events;
using Content.Shared.Automation;
using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Map.Components;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Physics.Systems;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class AsteroidFieldSystem : EntitySystem
{
    [Dependency] private IMapManager _mapManager = default!;
    [Dependency] private SharedMapSystem _mapSystem = default!;
    [Dependency] private ITileDefinitionManager _tileDef = default!;
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private IPrototypeManager _proto = default!;
    [Dependency] private EntityLookupSystem _lookup = default!;
    [Dependency] private SharedTransformSystem _transform = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<RoundStartingEvent>(OnRoundStarted);
    }

    private void OnRoundStarted(RoundStartingEvent ev)
    {
        var query = EntityQueryEnumerator<AsteroidFieldComponent, MapComponent>();
        while (query.MoveNext(out var uid, out var field, out _))
        {
            GenerateField(uid, field);
        }
    }

    /// <summary>
    /// Генерирует все обломки вокруг карты.
    /// </summary>
    public void GenerateField(EntityUid mapUid, AsteroidFieldComponent field)
    {
        for (int i = 0; i < field.DebrisCount; i++)
            GenerateDebris(mapUid, field);
    }

    /// <summary>
    /// Создаёт один обломок.
    /// </summary>
    public void GenerateDebris(EntityUid mapUid, AsteroidFieldComponent field)
    {
        var mapId = Comp<MapComponent>(mapUid).MapId;

        var angle = _random.NextFloat(0, MathF.PI * 2);
        var distance = _random.NextFloat(field.MinRadius, field.MaxRadius);
        var pos = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * distance;

        // Создаём пустую сущность для грида
        EntityUid gridUid = Spawn(null, MapCoordinates.Nullspace);
        var grid = EnsureComp<MapGridComponent>(gridUid);
        _transform.SetParent(gridUid, mapUid);
        var entityGrid = new Entity<MapGridComponent>(gridUid, grid);

        int w = _random.Next(field.MinSize, field.MaxSize);
        int h = _random.Next(field.MinSize, field.MaxSize);

        var shape = GenerateAsteroidShape(w, h, 0.7f);
        var baseTile = new Tile(_tileDef[field.BaseTile].TileId);

        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                if (shape[x, y])
                    _mapSystem.SetTile(entityGrid, new Vector2i(x, y), baseTile);

        PlaceOreEntities(entityGrid, shape, field, w, h);

        _transform.SetWorldPosition(gridUid, pos);
    }

    private bool[,] GenerateAsteroidShape(int w, int h, float initialFill)
    {
        bool[,] cells = new bool[w, h];
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                cells[x, y] = _random.Prob(initialFill);

        for (int pass = 0; pass < 3; pass++)
        {
            bool[,] next = new bool[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                {
                    int n = NeighborCount(cells, x, y, w, h);
                    if (cells[x, y])
                        next[x, y] = n >= 2;
                    else
                        next[x, y] = n >= 3;
                }
            cells = next;
        }
        return cells;
    }

    private int NeighborCount(bool[,] grid, int cx, int cy, int w, int h)
    {
        int count = 0;
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                int nx = cx + dx, ny = cy + dy;
                if (nx >= 0 && nx < w && ny >= 0 && ny < h && grid[nx, ny])
                    count++;
            }
        return count;
    }

    private void PlaceOreEntities(Entity<MapGridComponent> entityGrid, bool[,] shape,
        AsteroidFieldComponent field, int w, int h)
    {
        var validPositions = new List<Vector2i>();
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                if (shape[x, y])
                    validPositions.Add(new Vector2i(x, y));

        _random.Shuffle(validPositions);

        foreach (var entry in field.OreEntries)
        {
            int placed = 0;
            foreach (var tilePos in validPositions)
            {
                if (placed >= entry.MaxPerDebris)
                    break;

                if (TileHasOreEntity(entityGrid, tilePos))
                    continue;

                if (_random.Prob(entry.Weight / 100f))
                {
                    var coords = _mapSystem.GridTileToLocal(entityGrid.Owner, entityGrid.Comp, tilePos);
                    Spawn(entry.Prototype, coords);
                    placed++;
                }
            }
        }
    }

    private bool TileHasOreEntity(Entity<MapGridComponent> entityGrid, Vector2i tilePos)
    {
        var center = _mapSystem.TileCenterToVector(entityGrid, tilePos);
        var worldPos = _mapSystem.LocalToWorld(entityGrid.Owner, entityGrid.Comp, center);
        var mapCoords = new MapCoordinates(worldPos, Transform(entityGrid.Owner).MapID);
        var entities = _lookup.GetEntitiesInRange(mapCoords, 0.5f);
        return entities.Any(e => HasComp<DrillableTileComponent>(e));
    }
}
