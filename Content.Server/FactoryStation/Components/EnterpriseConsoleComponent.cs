using Robust.Shared.GameObjects;

namespace Content.Server.FactoryStation.Components;

[RegisterComponent]
public sealed partial class EnterpriseConsoleComponent : Component
{
    /// <summary>
    /// Радиус сканирования. -1 = все машины на карте.
    /// </summary>
    [DataField]
    public float Range { get; set; } = -1f;

    /// <summary>
    /// Интервал обновления данных (в секундах).
    /// </summary>
    [DataField]
    public float UpdateInterval { get; set; } = 1f;

    /// <summary>
    /// Внутренний таймер для системы.
    /// </summary>
    public float UpdateAccumulator { get; set; }
}
