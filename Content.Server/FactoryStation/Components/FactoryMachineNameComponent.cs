using Robust.Shared.GameObjects;

namespace Content.Server.FactoryStation.Components;

/// <summary>
/// Задаёт отображаемое имя машины в консоли предприятия.
/// </summary>
[RegisterComponent]
public sealed partial class FactoryMachineNameComponent : Component
{
    [DataField]
    public string Name { get; set; } = string.Empty;
}
