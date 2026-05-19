using Robust.Shared.Serialization;

namespace Content.Shared.Lathe;

[Serializable, NetSerializable]
public sealed class LatheProduceRecipeEvent : EntityEventArgs
{
    public string RecipeId;

    public LatheProduceRecipeEvent(string recipeId)
    {
        RecipeId = recipeId;
    }
}
