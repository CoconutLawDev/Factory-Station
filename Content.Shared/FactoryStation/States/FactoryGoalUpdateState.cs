using System;
using System.Collections.Generic;
using Robust.Shared.Serialization;

namespace Content.Shared.FactoryStation.States;

[Serializable, NetSerializable]
public sealed class FactoryGoalUpdateState : BoundUserInterfaceState
{
    public string? CurrentGoal;
    public int CurrentProgress;
    public List<string> AvailableGoals;
    public string? GoalName;
    public string? GoalDifficulty;
    public int RequiredAmount;

    public FactoryGoalUpdateState(
        string? currentGoal,
        int currentProgress,
        List<string> availableGoals,
        string? goalName = null,
        string? goalDifficulty = null,
        int requiredAmount = 0)
    {
        CurrentGoal = currentGoal;
        CurrentProgress = currentProgress;
        AvailableGoals = availableGoals;
        GoalName = goalName;
        GoalDifficulty = goalDifficulty;
        RequiredAmount = requiredAmount;
    }
}
