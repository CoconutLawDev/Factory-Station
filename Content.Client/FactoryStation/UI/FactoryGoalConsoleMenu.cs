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

public sealed class FactoryGoalConsoleMenu : DefaultWindow
{
    public event Action<string>? OnGoalSelected;

    private readonly BoxContainer _goalsContainer;
    private readonly RichTextLabel _currentGoalLabel;
    private FactoryGoalUpdateState? _lastState;
    private double _displayedTime;

    public FactoryGoalConsoleMenu()
    {
        Title = "Консоль промышленных контрактов";
        MinSize = SetSize = new Vector2(520, 520);

        var root = new BoxContainer
        {
            Orientation = BoxContainer.LayoutOrientation.Vertical,
            SeparationOverride = 8,
            Margin = new Thickness(10)
        };

        var header = new PanelContainer
        {
            PanelOverride = new StyleBoxFlat { BackgroundColor = Color.FromHex("#20252f") },
            MinHeight = 60
        };

        var headerText = new RichTextLabel { Margin = new Thickness(8) };
        headerText.SetMarkup(
            "[font size=16][color=#5da9ff]ЦЕНТРАЛЬНОЕ КОМАНДОВАНИЕ[/color][/font]\n" +
            "Панель управления промышленными контрактами");

        header.AddChild(headerText);
        root.AddChild(header);

        _currentGoalLabel = new RichTextLabel { Margin = new Thickness(4) };
        _currentGoalLabel.SetMarkup("[color=#aaaaaa]Текущий контракт отсутствует[/color]");
        root.AddChild(_currentGoalLabel);

        var availableLabel = new Label
        {
            Text = "Доступные контракты:",
            FontColorOverride = Color.FromHex("#8fb8ff")
        };
        root.AddChild(availableLabel);

        var scroll = new ScrollContainer { VerticalExpand = true };
        _goalsContainer = new BoxContainer
        {
            Orientation = BoxContainer.LayoutOrientation.Vertical,
            SeparationOverride = 6
        };
        scroll.AddChild(_goalsContainer);
        root.AddChild(scroll);

        ContentsContainer.AddChild(root);
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
        _goalsContainer.RemoveAllChildren();

        if (_lastState?.CurrentGoal != null)
        {
            var color = _lastState.GoalDifficulty switch
            {
                "Light" => "#5EFF7A",
                "Medium" => "#FFD95E",
                "Hard" => "#FF6B6B",
                _ => "#FFFFFF"
            };

            var timeText = FormatTime(_displayedTime);

            _currentGoalLabel.SetMarkup(
                $"[font size=14][color={color}]АКТИВНЫЙ КОНТРАКТ[/color][/font]\n" +
                $"Контракт: [bold]{_lastState.GoalName ?? _lastState.CurrentGoal}[/bold]\n" +
                $"Приоритет: {_lastState.GoalDifficulty}\n" +
                $"Прогресс поставок: [color=#5da9ff]{_lastState.CurrentProgress}/{_lastState.RequiredAmount}[/color]\n" +
                $"Оставшееся время: [color=#FFD95E]{timeText}[/color]");
        }
        else
        {
            _currentGoalLabel.SetMarkup("[color=#aaaaaa]Текущий контракт отсутствует[/color]");
        }

        var hasProgress = _lastState?.CurrentGoal != null && _lastState?.CurrentProgress > 0;

        if (_lastState?.AvailableGoals != null)
        {
            foreach (var goal in _lastState.AvailableGoals)
            {
                var color = goal.Difficulty switch
                {
                    "Light" => "#5EFF7A",
                    "Medium" => "#FFD95E",
                    "Hard" => "#FF6B6B",
                    _ => "#FFFFFF"
                };

                var panel = new PanelContainer
                {
                    PanelOverride = new StyleBoxFlat { BackgroundColor = Color.FromHex("#252b36") }
                };

                var container = new BoxContainer
                {
                    Orientation = BoxContainer.LayoutOrientation.Horizontal,
                    SeparationOverride = 8,
                    Margin = new Thickness(6)
                };

                var label = new RichTextLabel { HorizontalExpand = true };
                label.SetMarkup($"[color={color}]{goal.Name}[/color]");

                var difficultyLabel = new Label
                {
                    Text = goal.Difficulty,
                    FontColorOverride = Color.FromHex(color),
                    MinWidth = 80
                };

                var button = new Button
                {
                    Text = "Принять",
                    MinWidth = 100
                };

                button.OnPressed += _ =>
                {
                    if (hasProgress)
                    {
                        var dialog = new GoalConfirmationDialog(goal.Name, _lastState.CurrentProgress);
                        dialog.OnConfirmed += () => OnGoalSelected?.Invoke(goal.Id);
                        dialog.OpenCentered();
                    }
                    else
                    {
                        OnGoalSelected?.Invoke(goal.Id);
                    }
                };

                container.AddChild(label);
                container.AddChild(difficultyLabel);
                container.AddChild(button);

                panel.AddChild(container);
                _goalsContainer.AddChild(panel);
            }
        }
    }

    private static string FormatTime(double seconds)
    {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return timeSpan.TotalHours >= 1
            ? $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}"
            : $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }
}
