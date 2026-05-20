using Robust.Shared.Audio;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Server.FactoryStation.Components;

[RegisterComponent]
public sealed partial class FactoryIndustrialHeatComponent : Component
{
    /// <summary>
    /// Текущая температура станка.
    /// </summary>
    [DataField]
    public float CurrentHeat = 20f;

    /// <summary>
    /// Максимальная температура.
    /// </summary>
    [DataField]
    public float MaxHeat = 1500f;

    /// <summary>
    /// Нагрев во время работы.
    /// </summary>
    [DataField]
    public float HeatPerSecond = 55f;

    /// <summary>
    /// Охлаждение в простое.
    /// </summary>
    [DataField]
    public float CooldownPerSecond = 8f;

    /// <summary>
    /// Температура появления дыма.
    /// </summary>
    [DataField]
    public float SmokeThreshold = 200f;

    /// <summary>
    /// Опасная температура.
    /// </summary>
    [DataField]
    public float DangerThreshold = 900f;

    /// <summary>
    /// Производит ли дым.
    /// </summary>
    [DataField]
    public bool ProducingSmoke = true;

    /// <summary>
    /// Интервал появления дыма.
    /// </summary>
    [DataField]
    public float SmokeInterval = 3f;

    /// <summary>
    /// Нагрев атмосферы вокруг станка.
    /// </summary>
    [DataField]
    public float AtmosHeatPerSecond = 25f;

    /// <summary>
    /// Интервал распространения дыма.
    /// </summary>
    [DataField]
    public float SmokeSpreadInterval = 2f;

    /// <summary>
    /// Радиус распространения дыма.
    /// </summary>
    [DataField]
    public float SmokeRadius = 3f;

    /// <summary>
    /// Таймер дыма.
    /// </summary>
    public float SmokeAccumulator = 0f;

    /// <summary>
    /// Зацикленный звук работы станка.
    /// </summary>
    [DataField]
    public SoundSpecifier? RunningSound;

    /// <summary>
    /// Audio stream entity.
    /// </summary>
    public EntityUid? AudioStream;
}
