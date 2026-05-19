using System.Collections.Generic;
using Content.Shared.FactoryStation.Prototypes;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.FactoryStation.Components;

[RegisterComponent]
public sealed partial class FactoryGoalComponent : Component
{
    [DataField]
    public ProtoId<FactoryGoalPrototype>? CurrentGoal;

    [DataField]
    public int CurrentProgress = 0;

    [DataField]
    public List<ProtoId<FactoryGoalPrototype>> AvailableGoals = new();

    // Уже использованные цели за раунд
    [DataField]
    public HashSet<ProtoId<FactoryGoalPrototype>> UsedGoals = new();
}
