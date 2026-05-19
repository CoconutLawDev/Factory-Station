using System;
using Robust.Shared.Serialization;

namespace Content.Shared.FactoryStation.Messages;

[Serializable, NetSerializable]
public sealed class RequestFactoryGoalStateMessage : BoundUserInterfaceMessage
{
}
