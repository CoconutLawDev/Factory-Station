using Lidgren.Network;
using Robust.Shared.Serialization;

namespace Content.Shared.Lathe;

/// <summary>
/// Сообщение от клиента к серверу для установки активного рецепта на станке.
/// </summary>
[Serializable, NetSerializable]
public sealed class LatheSetActiveRecipeMessage : BoundUserInterfaceMessage
{
    public string? RecipeId;
    public bool Enabled;

    public LatheSetActiveRecipeMessage(string? recipeId, bool enabled)
    {
        RecipeId = recipeId;
        Enabled = enabled;
    }
}
