using Content.Server.FactoryStation.Components;
using Robust.Shared.Containers;
using System;

namespace Content.Server.FactoryStation.Systems;

public sealed partial class HeatSinkSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<FactoryIndustrialHeatComponent, EntInsertedIntoContainerMessage>(OnItemInserted);
        SubscribeLocalEvent<FactoryIndustrialHeatComponent, EntRemovedFromContainerMessage>(OnItemRemoved);
    }

    private void OnItemInserted(EntityUid uid, FactoryIndustrialHeatComponent heat, EntInsertedIntoContainerMessage args)
    {
        if (args.Container.ID != "heat_sink")
            return;

        if (!TryComp<HeatSinkComponent>(args.Entity, out var sink))
            return;

        // Защита от дублирования — если в контейнере уже есть предметы, корректируем
        if (args.Container.ContainedEntities.Count > 1)
        {
            heat.AmbientCoolingCoefficient -= sink.CoolingBonus * (args.Container.ContainedEntities.Count - 1);
        }

        heat.AmbientCoolingCoefficient += sink.CoolingBonus;
        heat.AmbientCoolingCoefficient = Math.Max(heat.AmbientCoolingCoefficient, 0.1f);
    }

    private void OnItemRemoved(EntityUid uid, FactoryIndustrialHeatComponent heat, EntRemovedFromContainerMessage args)
    {
        if (args.Container.ID != "heat_sink")
            return;

        if (!TryComp<HeatSinkComponent>(args.Entity, out var sink))
            return;

        heat.AmbientCoolingCoefficient -= sink.CoolingBonus;
        heat.AmbientCoolingCoefficient = Math.Max(heat.AmbientCoolingCoefficient, 0.1f);
    }
}
