using System.Collections.Generic;
using Content.Shared.FactoryStation;
using Robust.Shared.GameObjects;

namespace Content.Server.FactoryStation.Components;

[RegisterComponent]
public sealed partial class EnterpriseConsoleDataComponent : Component
{
    public List<MachineInfo> Machines { get; set; } = new();
}
