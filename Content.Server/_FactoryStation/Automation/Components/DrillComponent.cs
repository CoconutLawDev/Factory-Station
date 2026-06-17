using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Server.Automation;

[RegisterComponent]
public sealed partial class DrillComponent : Component
{
    [DataField]
    public bool Enabled = false;

    [DataField]
    public float Interval = 5f;

    /// <summary>
    /// Максимальное количество руды на тайле, после которого бур останавливается.
    /// </summary>
    [DataField]
    public int MaxOreItemsOnTile = 60;

    public TimeSpan LastDrillTime;
}
