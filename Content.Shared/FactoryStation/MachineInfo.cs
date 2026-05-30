using System.Collections.Generic;
using Robust.Shared.Serialization;

namespace Content.Shared.FactoryStation;

[Serializable, NetSerializable]
public sealed partial class MachineInfo
{
    public NetEntity Entity;
    public string Name;
    public string? ActiveRecipe;
    public Dictionary<string, int> Materials;
    public float? Temperature;
    public int Damage;
    public bool Powered;
    public MachineStatus Status;

    public MachineInfo(NetEntity entity, string name, string? activeRecipe, Dictionary<string, int> materials,
        float? temperature, int damage, bool powered, MachineStatus status)
    {
        Entity = entity;
        Name = name;
        ActiveRecipe = activeRecipe;
        Materials = materials;
        Temperature = temperature;
        Damage = damage;
        Powered = powered;
        Status = status;
    }
}

[Serializable, NetSerializable]
public enum MachineStatus : byte
{
    Normal,
    Warning,
    Critical,
    Offline
}
