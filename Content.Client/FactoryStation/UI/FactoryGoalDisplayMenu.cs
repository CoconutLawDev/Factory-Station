using System;
using System.Numerics;
using Content.Client.Message;
using Content.Shared.FactoryStation.States;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Maths;
using Robust.Shared.Timing;

namespace Content.Client.FactoryStation.UI;

public sealed class FactoryGoalDisplayMenu : DefaultWindow
{
    private readonly RichTextLabel _goalInfo;
    private readonly ProgressBar _progress;
    private FactoryGoalUpdateState? _lastState;
    private double _displayedTime;

    public FactoryGoalDisplayMenu()
    {
        Title = "Монитор промышленного контракта";
        MinSize = SetSize = new Vector2(460, 260);

        var root = new BoxContainer
        {
            Orientation = BoxContainer.LayoutOrientation.Vertical,
            SeparationOverride = 10,
            Margin = new Thickness(12)
        };

        var panel = new PanelContainer
        {
            PanelOverride = new StyleBoxFlat { BackgroundColor = Color.FromHex("#1f2430") }
        };

        _goalInfo = new RichTextLabel { Margin = new Thickness(10) };
        panel.AddChild(_goalInfo);

        root.AddChild(panel);

        _progress = new ProgressBar { MinHeight = 24 };
        root.AddChild(_progress);

        ContentsContainer.AddChild(root);

        SetNoGoal();
    }

    private void SetNoGoal()
    {
        _goalInfo.SetMarkupPermissive("[font size=16][color=#aaaaaa]АКТИВНЫХ КОНТРАКТОВ НЕТ[/color][/font]");
        _progress.MaxValue = 1;
        _progress.Value = 0;
    }

    public void UpdateState(FactoryGoalUpdateState state)
    {
        _lastState = state;
        _displayedTime = state.RemainingTime;
        RefreshUI();
    }

    protected override void FrameUpdate(FrameEventArgs args)
    {
        base.FrameUpdate(args);
        if (_lastState?.CurrentGoal == null || _displayedTime <= 0)
            return;

        _displayedTime = Math.Max(0, _displayedTime - args.DeltaSeconds);
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (_lastState?.CurrentGoal == null)
        {
            SetNoGoal();
            return;
        }

        var color = _lastState.GoalDifficulty switch
        {
            "Light" => "#5EFF7A",
            "Medium" => "#FFD95E",
            "Hard" => "#FF6B6B",
            _ => "#FFFFFF"
        };

        var timeText = FormatTime(_displayedTime);

        _goalInfo.SetMarkupPermissive(
            $"[font size=16][color={color}]АКТИВНЫЙ ПРОМЫШЛЕННЫЙ КОНТРАКТ[/color][/font]\n\n" +
            $"Контракт: [bold]{_lastState.GoalName ?? _lastState.CurrentGoal}[/bold]\n" +
            $"Приоритет: {_lastState.GoalDifficulty}\n" +
            $"Выполнение: {_lastState.CurrentProgress}/{_lastState.RequiredAmount}\n" +
            $"Оставшееся время: {timeText}");

        _progress.MaxValue = _lastState.RequiredAmount;
        _progress.Value = _lastState.CurrentProgress;
    }

    private static string FormatTime(double seconds)
    {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return timeSpan.TotalHours >= 1
            ? $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}"
            : $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }
}
