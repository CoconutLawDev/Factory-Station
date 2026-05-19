using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.Automation;

[RegisterComponent]
public sealed partial class ActiveRecipeComponent : Component
{
    /// <summary>
    /// ID выбранного рецепта. Если null — авто-производство неактивно.
    /// </summary>
    [DataField("activeRecipeId")]
    public string? ActiveRecipeId;

    /// <summary>
    /// Включён ли режим постоянного автопроизводства.
    /// </summary>
    [DataField("enabled")]
    public bool Enabled = true;
}
