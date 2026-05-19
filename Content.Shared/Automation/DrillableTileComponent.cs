using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.Automation;

[RegisterComponent]
public sealed partial class DrillableTileComponent : Component
{
    [DataField]
    public EntProtoId SpawnPrototype; // Убрано значение по умолчанию

    [DataField]
    public int AmountPerDrill = 1;

    [DataField]
    public int TotalAmount = 100;
}
