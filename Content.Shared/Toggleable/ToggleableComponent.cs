using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.Toggleable;

[RegisterComponent]
public sealed partial class ToggleableComponent : Component
{
    [DataField("enabled")]
    public bool Enabled = false;
}
