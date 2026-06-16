using Content.Server.FactoryStation.Components;
using Content.Server.Popups;
using Content.Shared.Item;
using Robust.Shared.Containers;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class HeatSinkSystem : EntitySystem
{
    [Dependency] private PopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();

        // Событие вставки предмета в слот
        SubscribeLocalEvent<FactoryIndustrialHeatComponent, EntInsertedIntoContainerMessage>(OnItemInserted);
        // Событие извлечения предмета из слота
        SubscribeLocalEvent<FactoryIndustrialHeatComponent, EntRemovedFromContainerMessage>(OnItemRemoved);
    }

    private void OnItemInserted(EntityUid uid, FactoryIndustrialHeatComponent heat, EntInsertedIntoContainerMessage args)
    {
        // Проверяем, что вставили именно в слот "heat_sink"
        if (args.Container.ID != "heat_sink")
            return;

        // Проверяем, что вставленный предмет — радиаторная пластина
        if (!TryComp<HeatSinkComponent>(args.Entity, out var sink))
            return;

        // Увеличиваем коэффициент охлаждения
        heat.AmbientCoolingCoefficient += sink.CoolingBonus;
    }

    private void OnItemRemoved(EntityUid uid, FactoryIndustrialHeatComponent heat, EntRemovedFromContainerMessage args)
    {
        if (args.Container.ID != "heat_sink")
            return;

        if (!TryComp<HeatSinkComponent>(args.Entity, out var sink))
            return;

        // Уменьшаем коэффициент обратно
        heat.AmbientCoolingCoefficient -= sink.CoolingBonus;
    }
}
