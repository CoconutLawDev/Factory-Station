using Robust.Shared.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.Automation;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ActiveRecipeComponent : Component
{
    [DataField("activeRecipeId"), AutoNetworkedField]
    public string? ActiveRecipeId;

    [DataField("enabled"), AutoNetworkedField]
    public bool Enabled = true;

    [DataField("activeRecipeName"), AutoNetworkedField]
    public string? ActiveRecipeName;
}
