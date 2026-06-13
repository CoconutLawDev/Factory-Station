using System.Collections.Generic;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.Server.FactoryStation.Components;

/// <summary>
/// При старте карты с заданной вероятностью заменяет эту стену на одну из кастомных клеток.
/// </summary>
[RegisterComponent]
public sealed partial class RandomDrillVeinComponent : Component
{
    /// <summary>
    /// Список прототипов клеток, на которые может быть заменена стена.
    /// </summary>
    [DataField(required: true)]
    public List<EntProtoId> VeinPrototypes { get; set; } = new();

    /// <summary>
    /// Вероятность замены (от 0 до 1).
    /// </summary>
    [DataField]
    public float Chance { get; set; } = 0.02f;
}
