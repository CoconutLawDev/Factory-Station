using System.Linq;
using Content.Server.Materials;
using Content.Server.Players;
using Content.Shared;
using Content.Shared.Automation;
using Content.Shared.Mind;
using Content.Shared.Materials;
using Content.Shared.Whitelist;
using Robust.Shared.GameObjects;
using Robust.Shared.GameObjects.Components;
using Robust.Shared.Map;
using Robust.Shared.Physics.Components;
using Robust.Shared.Physics.Systems;
using Robust.Shared.Timing;
using Robust.Shared.Player;

namespace Content.Server.Automation;

// 1. Делаем класс partial, чтобы удовлетворить требования к [Dependency] полям.
// 2. Используем современные пространства имён (MindComponent, ActorComponent).
public sealed partial class AutoCollectorSystem : EntitySystem
{
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private EntityWhitelistSystem _whitelistSystem = default!;
    [Dependency] private SharedPhysicsSystem _physicsSystem = default!;
    [Dependency] private SharedTransformSystem _transformSystem = default!;
    // 3. Используем серверную MaterialStorageSystem для вызова TryInsertMaterialEntity
    [Dependency] private MaterialStorageSystem _materialStorageSystem = default!;
    [Dependency] private EntityLookupSystem _entityLookup = default!;

    private readonly Dictionary<EntityUid, TimeSpan> _nextCheck = new();
    // 4. Используем современный HashSet, совместимый с EntityLookupSystem
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
        foreach (var (uid, next) in _nextCheck.ToArray())
        {
            if (curTime < next) continue;
            if (!TryComp<AutoCollectorComponent>(uid, out var collector) || collector.Deleted)
            {
                _nextCheck.Remove(uid);
                continue;
            }
            _nextCheck[uid] = curTime + TimeSpan.FromSeconds(collector.Interval);
            TryCollectItems(uid, collector);
        }
    }

    private void TryCollectItems(EntityUid uid, AutoCollectorComponent collector)
    {
        if (!TryComp<MaterialStorageComponent>(uid, out var storage))
            return;

        // 5. Получаем координаты карты через TransformSystem (вместо устаревшего MapPosition)
        var mapCoords = _transformSystem.GetMapCoordinates(uid);

        // 6. Ищем сущности в радиусе ~0.7 с флагами Dynamic | Static
        _entitySet.Clear();
        _entityLookup.GetEntitiesInRange(mapCoords, 0.7f, _entitySet, LookupFlags.Dynamic | LookupFlags.Static);

        foreach (var entity in _entitySet)
        {
            if (entity.Owner == uid) continue;

            // 7. Фильтрация игроков: используем MindComponent и ActorComponent
            if (HasComp<MindComponent>(entity.Owner) || HasComp<ActorComponent>(entity.Owner))
                continue;

            if (!HasComp<PhysicsComponent>(entity.Owner)) continue;
            if (!CanInsert(uid, entity.Owner, storage)) continue;
            InsertItem(uid, entity.Owner, storage, collector.PullForce);
        }
    }

    private bool CanInsert(EntityUid station, EntityUid item, MaterialStorageComponent storage)
    {
        if (storage.Whitelist != null && !_whitelistSystem.IsWhitelistPass(storage.Whitelist, item))
            return false;

        var totalStored = storage.Storage.Values.Sum();
        var storageLimit = storage.StorageLimit ?? int.MaxValue;
        return totalStored < storageLimit;
    }

    private void InsertItem(EntityUid station, EntityUid item, MaterialStorageComponent storage, float pullForce)
    {
        // 8. Правильный вызов ApplyLinearImpulse: передаём uid и PhysicsComponent отдельно
        if (TryComp<PhysicsComponent>(item, out var physics))
        {
            var stationPos = _transformSystem.GetWorldPosition(station);
            var itemPos = _transformSystem.GetWorldPosition(item);
            var direction = stationPos - itemPos;
            if (direction.Length() > 0.01f)
            {
                direction = direction.Normalized();
                _physicsSystem.ApplyLinearImpulse(item, direction * pullForce, body: physics);
            }
        }

        // 9. Правильная сигнатура TryInsertMaterialEntity (user, toInsert, receiver, component)
        if (_materialStorageSystem.TryInsertMaterialEntity(station, item, station, storage))
        {
            QueueDel(item);
        }
    }
}
