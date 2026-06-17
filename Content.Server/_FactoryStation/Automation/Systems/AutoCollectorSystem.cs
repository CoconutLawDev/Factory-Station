using System.Linq;
using Content.Server.Materials;
using Content.Shared.Automation;
using Content.Shared.Materials;
using Robust.Shared.Physics.Components;
using Robust.Shared.Physics.Systems;
using Robust.Shared.Timing;

namespace Content.Server.Automation;

public sealed partial class AutoCollectorSystem : EntitySystem
{
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private SharedPhysicsSystem _physicsSystem = default!;
    [Dependency] private SharedTransformSystem _transformSystem = default!;
    [Dependency] private MaterialStorageSystem _materialStorageSystem = default!;
    [Dependency] private EntityLookupSystem _entityLookup = default!;

    private readonly Dictionary<EntityUid, TimeSpan> _nextCheck = new();
    private readonly HashSet<Entity<TransformComponent>> _entitySet = new();

    public override void Initialize()
    {
        SubscribeLocalEvent<AutoCollectorComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<AutoCollectorComponent, ComponentShutdown>(OnShutdown);
    }

    private void OnStartup(EntityUid uid, AutoCollectorComponent component, ComponentStartup args)
    {
        _nextCheck[uid] = _timing.CurTime + TimeSpan.FromSeconds(component.Interval);
    }

    private void OnShutdown(EntityUid uid, AutoCollectorComponent component, ComponentShutdown args)
    {
        _nextCheck.Remove(uid);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);
        var curTime = _timing.CurTime;

        // Притягиваем предметы каждый тик
        foreach (var (uid, _) in _nextCheck.ToArray())
        {
            if (!TryComp<AutoCollectorComponent>(uid, out var collector) || collector.Deleted)
            {
                _nextCheck.Remove(uid);
                continue;
            }
            TryCollectItems(uid, collector);
        }

        // Проверка на вставку — раз в интервал
        foreach (var (uid, next) in _nextCheck.ToArray())
        {
            if (curTime < next) continue;
            if (!TryComp<AutoCollectorComponent>(uid, out var collector) || collector.Deleted)
            {
                _nextCheck.Remove(uid);
                continue;
            }
            _nextCheck[uid] = curTime + TimeSpan.FromSeconds(collector.Interval);
            TryInsertItems(uid, collector);
        }
    }

    private void TryCollectItems(EntityUid uid, AutoCollectorComponent collector)
    {
        var mapCoords = _transformSystem.GetMapCoordinates(uid);
        _entitySet.Clear();
        _entityLookup.GetEntitiesInRange(mapCoords, 0.7f, _entitySet, LookupFlags.Dynamic);

        foreach (var entity in _entitySet)
        {
            if (entity.Owner == uid) continue;
            if (!HasComp<PhysicsComponent>(entity.Owner)) continue;

            var stationPos = _transformSystem.GetWorldPosition(uid);
            var itemPos = _transformSystem.GetWorldPosition(entity.Owner);
            var direction = stationPos - itemPos;
            var distance = direction.Length();

            if (distance > 0.01f && distance <= collector.CollectionRadius)
            {
                direction = direction.Normalized();
                if (TryComp<PhysicsComponent>(entity.Owner, out var physics))
                {
                    // Притягиваем каждый тик
                    _physicsSystem.ApplyLinearImpulse(entity.Owner, direction * collector.PullForce, body: physics);
                }
            }
        }
    }

    private void TryInsertItems(EntityUid uid, AutoCollectorComponent collector)
    {
        if (!TryComp<MaterialStorageComponent>(uid, out var storage))
            return;

        var mapCoords = _transformSystem.GetMapCoordinates(uid);
        _entitySet.Clear();
        _entityLookup.GetEntitiesInRange(mapCoords, 0.3f, _entitySet, LookupFlags.Dynamic);

        foreach (var entity in _entitySet)
        {
            if (entity.Owner == uid) continue;
            if (TerminatingOrDeleted(entity.Owner)) continue;
            if (!HasComp<PhysicsComponent>(entity.Owner)) continue;
            if (!CanInsert(uid, entity.Owner, storage)) continue;

            if (_materialStorageSystem.TryInsertMaterialEntity(uid, entity.Owner, uid, storage))
            {
                QueueDel(entity.Owner);
            }
        }
    }

    private bool CanInsert(EntityUid station, EntityUid item, MaterialStorageComponent storage)
    {
        var totalStored = storage.Storage.Values.Sum();
        var storageLimit = storage.StorageLimit ?? int.MaxValue;
        return totalStored < storageLimit;
    }
}
