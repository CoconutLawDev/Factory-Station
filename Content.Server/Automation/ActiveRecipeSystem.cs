using System.Linq;
using Content.Server.Lathe.Components;
using Content.Server.Materials;
using Content.Shared.Automation;
using Content.Shared.Lathe;
using Content.Shared.Materials;
using Content.Shared.Research.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.Automation;

public sealed partial class ActiveRecipeSystem : EntitySystem
{
    [Dependency] private IPrototypeManager _proto = default!;
    [Dependency] private MaterialStorageSystem _materialStorage = default!;

    private float _accumulator;

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        _accumulator += frameTime;

        // Проверка раз в секунду
        if (_accumulator < 1f)
            return;

        _accumulator = 0f;

        var query = EntityQueryEnumerator<
            ActiveRecipeComponent,
            LatheComponent,
            MaterialStorageComponent>();

        while (query.MoveNext(out var uid, out var active, out var lathe, out var storage))
        {
            if (!active.Enabled)
                continue;

            if (string.IsNullOrWhiteSpace(active.ActiveRecipeId))
                continue;

            // Уже работает
            if (lathe.CurrentRecipe != null)
                continue;

            // Уже есть очередь
            if (lathe.Queue.Count > 0)
                continue;

            if (!_proto.TryIndex<LatheRecipePrototype>(
                    active.ActiveRecipeId,
                    out var recipe))
                continue;

            if (!HasEnoughMaterials(uid, recipe))
                continue;

            RaiseLocalEvent(uid,
                new LatheQueueRecipeMessage(
                    active.ActiveRecipeId,
                    1));
        }
    }

    private bool HasEnoughMaterials(
        EntityUid uid,
        LatheRecipePrototype recipe)
    {
        if (recipe.Materials == null)
            return false;

        return recipe.Materials.All(material =>
        {
            var amount = _materialStorage.GetMaterialAmount(
                uid,
                material.Key);

            return amount >= material.Value;
        });
    }
}
