using System;
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
using Content.Shared.PowerCell.Components;
using Content.Shared.Whitelist;
using Robust.Shared.Random;
using Robust.Server.GameObjects;
using Content.Shared.Examine;

namespace Content.Server.Automation;

public sealed partial class DrillSystem : EntitySystem
{
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private SharedBatterySystem _battery = default!;
    [Dependency] private SharedMapSystem _mapSystem = default!;
    [Dependency] private EntityLookupSystem _lookup = default!;
    [Dependency] private PopupSystem _popup = default!;
    [Dependency] private TagSystem _tagSystem = default!;
    [Dependency] private ItemSlotsSystem _itemSlots = default!;
    [Dependency] private EntityWhitelistSystem _whitelistSystem = default!;
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private SharedAppearanceSystem _appearance = default!;

    private const float DrillRadius = 0.5f;
    private static readonly ProtoId<TagPrototype> OreTag = "Ore";

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DrillComponent, ActivateInWorldEvent>(OnActivate);
        SubscribeLocalEvent<DrillableTileComponent, ExaminedEvent>(OnExamined);
    }

    private void OnActivate(EntityUid uid, DrillComponent drill, ActivateInWorldEvent args)
    {
        var xform = Transform(uid);

        if (!xform.Anchored)
        {
            _popup.PopupEntity($"Устройство должно быть прикручено к полу перед использованием", uid);
            return;
        }

        if (!IsOnValidTile(uid, drill, xform))
        {
            _popup.PopupEntity($"Это устройство не может добывать данный тип ресурса", uid);
            return;
        }

        drill.Enabled = !drill.Enabled;

        var name = MetaData(uid).EntityName;
        _popup.PopupEntity(drill.Enabled
            ? $"Бур {name} начал добычу"
            : $"Бур {name} остановлен", uid);

        _appearance.SetData(uid, DrillVisuals.Running, drill.Enabled);
        args.Handled = true;
    }

    private void OnExamined(EntityUid uid, DrillableTileComponent component, ExaminedEvent args)
    {
        var remaining = component.TotalAmount;
        var maxAmount = component.MaxAmount;

        if (remaining <= 0)
        {
            args.PushMarkup("[color=red]Жила полностью исчерпана.[/color]");
        }
        else if (remaining < maxAmount * 0.25f)
        {
            args.PushMarkup($"[color=orange]Жила почти исчерпана. Осталось: {remaining}[/color]");
        }
        else if (remaining < maxAmount * 0.5f)
        {
            args.PushMarkup($"[color=yellow]Жила наполовину исчерпана. Осталось: {remaining}[/color]");
        }
        else
        {
            args.PushMarkup($"[color=green]Запасы жилы: {remaining}[/color]");
        }
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<DrillComponent, PowerCellSlotComponent, TransformComponent>();
        while (query.MoveNext(out var uid, out var drill, out var cellSlot, out var xform))
        {
            if (!drill.Enabled)
                continue;

            if (!xform.Anchored)
            {
                StopDrill(uid, drill, "откручен от пола");
                continue;
            }

            if (!IsOnValidTile(uid, drill, xform))
            {
                StopDrill(uid, drill, "несовместимый тип ресурса");
                continue;
            }

            if (_timing.CurTime - drill.LastDrillTime < TimeSpan.FromSeconds(drill.Interval))
                continue;

            if (!_itemSlots.TryGetSlot(uid, cellSlot.CellSlotId, out var slot) || slot.Item == null)
            {
                StopDrill(uid, drill, "нет батарейки");
                continue;
            }

            var batteryUid = slot.Item.Value;

            if (!TryComp<BatteryComponent>(batteryUid, out var battery))
            {
                StopDrill(uid, drill, "неисправная батарейка");
                continue;
            }

            var currentCharge = _battery.GetCharge(batteryUid);
            if (currentCharge < 10)
            {
                StopDrill(uid, drill, "низкий заряд");
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
                if (!TryComp<DrillableTileComponent>(entity, out var tileComp))
                    continue;

                if (drill.Whitelist != null && !_whitelistSystem.IsWhitelistPass(drill.Whitelist, entity))
                    continue;

                drillable = tileComp;
                drillableUid = entity;
                break;
            }

            if (drillable == null)
                continue;

            if (drillable.TotalAmount <= 0)
            {
                StopDrill(uid, drill, "жила исчерпана");
                continue;
            }

            var oreItems = 0;
            foreach (var entity in entities)
            {
                if (_tagSystem.HasTag(entity, OreTag))
                    oreItems++;
            }

            if (oreItems >= drill.MaxOreItemsOnTile)
            {
                StopDrill(uid, drill, "нет места для новой руды, очистите пространство!!!");
                continue;
            }

            _battery.SetCharge(batteryUid, currentCharge - 10);
            drillable.TotalAmount -= drillable.AmountPerDrill;
            Dirty(drillableUid, drillable);

            var spawnProto = drillable.SpawnPrototype;
            if (spawnProto == "OilFactorySheet1" && _random.Prob(0.2f))
                spawnProto = "GasFactorySheet1";

            Spawn(spawnProto, xform.Coordinates);

            drill.LastDrillTime = _timing.CurTime;
        }
    }

    private void StopDrill(EntityUid uid, DrillComponent drill, string reason)
    {
        drill.Enabled = false;
        _appearance.SetData(uid, DrillVisuals.Running, false);
        _popup.PopupEntity($"Бур {MetaData(uid).EntityName} остановлен: {reason}", uid);
    }

    private bool IsOnValidTile(EntityUid uid, DrillComponent drill, TransformComponent xform)
    {
        if (!TryComp<MapGridComponent>(xform.GridUid, out var grid))
            return false;

        var tile = _mapSystem.GetTileRef((xform.GridUid.Value, grid), xform.Coordinates);
        var center = _mapSystem.ToCenterCoordinates(tile);
        var entities = _lookup.GetEntitiesInRange(center, DrillRadius);

        foreach (var entity in entities)
        {
            if (!TryComp<DrillableTileComponent>(entity, out _))
                continue;

            if (drill.Whitelist == null || _whitelistSystem.IsWhitelistPass(drill.Whitelist, entity))
                return true;
        }

        return false;
    }
}
