using Robust.Shared.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.Automation;

[RegisterComponent, NetworkedComponent]
public sealed partial class DrillableTileComponent : Component
{
    [DataField]
    public EntProtoId SpawnPrototype;

    [DataField]
    public int AmountPerDrill = 1;

    [DataField]
    public int TotalAmount = 100;

    // FactoryStation-Edit: Max amount for examine
    [DataField]
    public int MaxAmount = 100;
}
