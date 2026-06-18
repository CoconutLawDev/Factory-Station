using System.Linq;
using Content.Server.Popups;
using Content.Shared.Automation;
using Content.Shared.Power.Components;
using Content.Shared.Power.EntitySystems;
using Robust.Shared.Map;
using Robust.Shared.Map.Components;
using Robust.Shared.Timing;
using Content.Shared.Tag;
using Content.Shared.Interaction;
using Robust.Shared.Prototypes;
using Content.Shared.Containers.ItemSlots;
using Content.Shared.PowerCell.Components; // ← добавить

namespace Content.Server.Automation;

public sealed partial class DrillSystem : EntitySystem
{
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private SharedBatterySystem _battery = default!;
    [Dependency] private SharedMapSystem _mapSystem = default!;
    [Dependency] private EntityLookupSystem _lookup = default!;
    [Dependency] private PopupSystem _popup = default!;
    [Dependency] private TagSystem _tagSystem = default!;
    [Dependency] private ItemSlotsSystem _itemSlots = default!; // ← добавить

    private const float DrillRadius = 0.5f;
    private static readonly ProtoId<TagPrototype> OreTag = "Ore";

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DrillComponent, ActivateInWorldEvent>(OnActivate);
    }

    private void OnActivate(EntityUid uid, DrillComponent drill, ActivateInWorldEvent args)
    {
        drill.Enabled = !drill.Enabled;

        var name = MetaData(uid).EntityName;
        if (drill.Enabled)
            _popup.PopupEntity($"Бур {name} начал добычу", uid);
        else
            _popup.PopupEntity($"Бур {name} остановлен", uid);

        args.Handled = true;
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        // Изменено: ищем DrillComponent и PowerCellSlotComponent вместо BatteryComponent
        var query = EntityQueryEnumerator<DrillComponent, PowerCellSlotComponent, TransformComponent>();
        while (query.MoveNext(out var uid, out var drill, out var cellSlot, out var xform))
        {
            if (!drill.Enabled)
                continue;

            if (_timing.CurTime - drill.LastDrillTime < TimeSpan.FromSeconds(drill.Interval))
                continue;

            // Получаем батарейку из слота
            if (!_itemSlots.TryGetSlot(uid, cellSlot.CellSlotId, out var slot) || slot.Item == null)
            {
                drill.Enabled = false;
                _popup.PopupEntity($"Бур {MetaData(uid).EntityName} остановлен: нет батарейки", uid);
                continue;
            }

            var batteryUid = slot.Item.Value;

            // Проверяем заряд через батарейку в слоте
            if (!TryComp<BatteryComponent>(batteryUid, out var battery))
            {
                drill.Enabled = false;
                _popup.PopupEntity($"Бур {MetaData(uid).EntityName} остановлен: неисправная батарейка", uid);
                continue;
            }

            var currentCharge = _battery.GetCharge(batteryUid);
            if (currentCharge < 10)
            {
                drill.Enabled = false;
                _popup.PopupEntity($"Бур {MetaData(uid).EntityName} остановлен: низкий заряд", uid);
                continue;
            }

            if (!TryComp<MapGridComponent>(xform.GridUid, out var grid))
                continue;

            var tile = _mapSystem.GetTileRef((xform.GridUid.Value, grid), xform.Coordinates);
            var center = _mapSystem.ToCenterCoordinates(tile);

            var entities = _lookup.GetEntitiesInRange(center, DrillRadius);

            DrillableTileComponent? drillable = null;
            EntityUid drillableUid = EntityUid.Invalid;
            foreach (var entity in entities)
            {
                if (TryComp<DrillableTileComponent>(entity, out var tileComp))
                {
                    drillable = tileComp;
                    drillableUid = entity;
                    break;
                }
            }

            if (drillable == null || drillable.TotalAmount <= 0)
                continue;

            int oreItems = 0;
            foreach (var entity in entities)
            {
                if (_tagSystem.HasTag(entity, OreTag))
                    oreItems++;
            }

            if (oreItems >= drill.MaxOreItemsOnTile)
            {
                drill.Enabled = false;
                _popup.PopupEntity($"Бур {MetaData(uid).EntityName} остановлен: нет места для новой руды, очистите пространство!!!", uid);
                continue;
            }

            // Разряжаем батарейку в слоте
            _battery.SetCharge(batteryUid, currentCharge - 10);
            drillable.TotalAmount -= drillable.AmountPerDrill;
            Dirty(drillableUid, drillable);

            Spawn(drillable.SpawnPrototype, xform.Coordinates);

            drill.LastDrillTime = _timing.CurTime;
        }
    }
}
