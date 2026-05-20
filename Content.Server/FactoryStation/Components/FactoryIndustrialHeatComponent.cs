using Robust.Shared.Audio;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.Prototypes;

namespace Content.Server.FactoryStation.Components;

[RegisterComponent]
public sealed partial class FactoryIndustrialHeatComponent : Component
{
    // --- Существующие поля (оставлены для контекста, но не дублируйте их) ---
    [DataField] public float CurrentHeat = 20f;
    [DataField] public float MaxHeat = 1500f;
    [DataField] public float HeatPerSecond = 55f;
    [DataField] public float CooldownPerSecond = 8f;
    [DataField] public float SmokeThreshold = 200f;
    [DataField] public float DangerThreshold = 900f;
    [DataField] public float CriticalThreshold = 1200f;
    [DataField] public bool ProducingSmoke = true;
    [DataField] public float SmokeInterval = 3f;
    [DataField] public float AtmosHeatPerSecond = 25f;
    [DataField] public float SmokeSpreadInterval = 2f;
    [DataField] public float SmokeRadius = 3f;
    [DataField] public float CO2PerSecond = 0.25f;
    [DataField] public float ExplosionChance = 0.05f;
    [DataField] public float ExplosionIntensity = 50f;
    [DataField] public float ExplosionSlope = 3f;
    [DataField] public float ExplosionMaxTileIntensity = 10f;
    [DataField] public float DamagePerSecondCritical = 15f;

    public float SmokeAccumulator = 0f;
    [DataField] public SoundSpecifier? RunningSound;
    public EntityUid? AudioStream;

    // --- Новые поля для тревоги ---
    /// <summary>
    /// Звук тревоги, включается при критическом перегреве (CriticalThreshold).
    /// </summary>
    [DataField] public SoundSpecifier? AlarmSound;

    /// <summary>
    /// Текущий stream сущности аварийного звука.
    /// </summary>
    public EntityUid? AlarmStream;

    // --- Новые поля для масла и искр ---
    /// <summary>
    /// Интервал (секунды) появления капель масла/искр при работе.
    /// </summary>
    [DataField] public float SpillageInterval = 5f;

    /// <summary>
    /// Прототип лужи масла (например "PuddleFactoryLubricant").
    /// </summary>
    [DataField] public EntProtoId SpillagePrototype = "PuddleFactoryLubricant";

    /// <summary>
    /// Шанс спавна лужи масла при срабатывании (0..1).
    /// </summary>
    [DataField] public float SpillageChance = 0.4f;

    /// <summary>
    /// Испускать ли искры вместе с маслом.
    /// </summary>
    [DataField] public bool EmitsSparks = true;

    /// <summary>
    /// Внутренний таймер для спавна масла.
    /// </summary>
    public float SpillageAccumulator;
}
