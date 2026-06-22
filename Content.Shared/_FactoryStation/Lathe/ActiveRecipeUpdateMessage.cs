using Robust.Shared.Serialization;

namespace Content.Shared.Lathe;

[Serializable, NetSerializable]
public sealed class ActiveRecipeUpdateMessage : BoundUserInterfaceMessage
{
    public string? RecipeName;

    public ActiveRecipeUpdateMessage(string? recipeName)
    {
        RecipeName = recipeName;
    }
}
