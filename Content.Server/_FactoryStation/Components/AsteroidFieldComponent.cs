using System.Collections.Generic;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Server.FactoryStation.Components;

[RegisterComponent]
public sealed partial class AsteroidFieldComponent : Component
{
    /// <summary> Количество обломков в поле. </summary>
    [DataField] public int DebrisCount = 6;

    /// <summary> Минимальный радиус от центра карты, где появляются обломки. </summary>
    [DataField] public float MinRadius = 20f;

    /// <summary> Максимальный радиус. </summary>
    [DataField] public float MaxRadius = 45f;

    /// <summary> Минимальный размер обломка в тайлах (сторона). </summary>
    [DataField] public int MinSize = 4;

    /// <summary> Максимальный размер. </summary>
    [DataField] public int MaxSize = 7;

    /// <summary> Тайл, используемый как пустая порода. </summary>
    [DataField] public string BaseTile = "FloorAsteroidSand";

    /// <summary> Список прототипов рудных клеток с весами вероятности и ограничениями. </summary>
    [DataField]
    public List<OreSpawnEntry> OreEntries = new()
    {
        new() { Prototype = "DrillTileIron",      Weight = 25, MaxPerDebris = 2 },
        new() { Prototype = "DrillTileCopper",    Weight = 20, MaxPerDebris = 2 },
        new() { Prototype = "DrillTileCoal",      Weight = 15, MaxPerDebris = 2 },
        new() { Prototype = "DrillTileSandStone", Weight = 10, MaxPerDebris = 1 },
    };
}

[DataDefinition]
public sealed partial class OreSpawnEntry
{
    [DataField(required: true)] public EntProtoId Prototype;
    [DataField] public float Weight = 1f;
    [DataField] public int MaxPerDebris = 2;
}
