using System.Linq;
using Content.Server.Lathe.Components;
using Content.Server.Materials;
using Content.Shared.Automation;
using Content.Shared.Lathe;
using Content.Shared.Materials;
using Content.Shared.Research.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Server.Automation;

public sealed partial class ActiveRecipeSystem : EntitySystem
{
    [Dependency] private IPrototypeManager _proto = default!;
    [Dependency] private MaterialStorageSystem _materialStorage = default!;
    [Dependency] private IGameTiming _timing = default!;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<ActiveRecipeComponent, LatheComponent, MaterialStorageComponent>();
        while (query.MoveNext(out var uid, out var active, out var lathe, out var storage))
        {
            if (!active.Enabled || string.IsNullOrEmpty(active.ActiveRecipeId))
                continue;

            if (lathe.CurrentRecipe != null || HasComp<LatheProducingComponent>(uid))
                continue;

            if (!_proto.TryIndex<LatheRecipePrototype>(active.ActiveRecipeId, out var recipe))
                continue;

            if (!HasEnoughMaterials(uid, recipe))
                continue;

            var msg = new LatheQueueRecipeMessage(active.ActiveRecipeId, 1);
            RaiseLocalEvent(uid, msg);
        }
    }

    private bool HasEnoughMaterials(EntityUid uid, LatheRecipePrototype recipe)
    {
        if (recipe.Materials == null)
            return false;

        return recipe.Materials.All(m =>
        {
            var amount = _materialStorage.GetMaterialAmount(uid, m.Key);
            return amount >= m.Value;
        });
    }
}
