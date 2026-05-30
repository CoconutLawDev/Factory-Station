using System.Collections.Generic;
using Robust.Shared.Serialization;

namespace Content.Shared.FactoryStation;

[Serializable, NetSerializable]
public sealed class EnterpriseConsoleBuiState : BoundUserInterfaceState
{
    public List<MachineInfo> Machines { get; }

    public EnterpriseConsoleBuiState(List<MachineInfo> machines)
    {
        Machines = machines;
    }
}
