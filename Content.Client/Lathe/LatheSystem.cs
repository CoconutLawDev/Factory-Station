using Robust.Client.GameObjects;
using Content.Shared.Lathe;
using Content.Shared.Power;
using Content.Client.Power;
using Content.Shared.Research.Prototypes;

namespace Content.Client.Lathe;

public sealed partial class LatheSystem : SharedLatheSystem
{
    [Dependency] private SharedAppearanceSystem _appearance = default!;
    [Dependency] private SpriteSystem _sprite = default!;

    // FactoryStation-Edit: Pending active recipe
    public string? PendingActiveRecipe;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<LatheComponent, AppearanceChangeEvent>(OnAppearanceChange);
        // FactoryStation-Edit: ActiveRecipe UI update
        SubscribeNetworkEvent<ActiveRecipeUpdateMessage>(OnActiveRecipeUpdate);
    }

    private void OnAppearanceChange(EntityUid uid, LatheComponent component, ref AppearanceChangeEvent args)
    {
        if (args.Sprite == null)
            return;

        if (_appearance.TryGetData<bool>(uid, LatheVisuals.IsRunning, out var isRunning, args.Component))
        {
            if (_sprite.LayerMapTryGet((uid, args.Sprite), LatheVisualLayers.IsRunning, out var runningLayer, false) &&
                component.RunningState != null &&
                component.IdleState != null)
            {
                var state = isRunning ? component.RunningState : component.IdleState;
                _sprite.LayerSetRsiState((uid, args.Sprite), runningLayer, state);
            }
        }

        if (_appearance.TryGetData<bool>(uid, PowerDeviceVisuals.Powered, out var powered, args.Component) &&
            _sprite.LayerMapTryGet((uid, args.Sprite), PowerDeviceVisualLayers.Powered, out var powerLayer, false))
        {
            _sprite.LayerSetVisible((uid, args.Sprite), powerLayer, powered);

            if (component.UnlitIdleState != null &&
                component.UnlitRunningState != null)
            {
                var state = isRunning ? component.UnlitRunningState : component.UnlitIdleState;
                _sprite.LayerSetRsiState((uid, args.Sprite), powerLayer, state);
            }
        }
    }

    // FactoryStation-Edit: ActiveRecipe UI update
    private void OnActiveRecipeUpdate(ActiveRecipeUpdateMessage args)
    {
        PendingActiveRecipe = args.RecipeName;
    }

    protected override bool HasRecipe(EntityUid uid, LatheRecipePrototype recipe, LatheComponent component)
    {
        return true;
    }
}

public enum LatheVisualLayers : byte
{
    IsRunning
}
