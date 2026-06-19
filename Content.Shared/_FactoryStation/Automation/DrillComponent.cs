using Robust.Shared.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.Manager.Attributes;
using Content.Shared.Whitelist;

namespace Content.Shared.Automation;

[RegisterComponent, NetworkedComponent]
public sealed partial class DrillComponent : Component
{
    [DataField]
    public int Interval = 5;

    [DataField]
    public int MaxOreItemsOnTile = 10;

    [DataField]
    public bool Enabled;

    [DataField]
    public TimeSpan LastDrillTime;

    // FactoryStation-Edit: Whitelist for drillable tiles
    [DataField]
    public EntityWhitelist? Whitelist;
}
