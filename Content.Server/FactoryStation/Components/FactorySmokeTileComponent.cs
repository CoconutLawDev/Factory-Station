using Robust.Shared.GameObjects;

namespace Content.Server.FactoryStation.Components;

[RegisterComponent]
public sealed partial class FactorySmokeTileComponent : Component
{
    public float Density = 1f;

    public float Lifetime = 15f;
}
