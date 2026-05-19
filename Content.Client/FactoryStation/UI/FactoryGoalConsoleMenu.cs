using System;
using System.Numerics;
using Content.Client.Message;
using Content.Shared.FactoryStation.States;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Maths;

namespace Content.Client.FactoryStation.UI;

public sealed class FactoryGoalConsoleMenu : DefaultWindow
{
    public event Action<string>? OnGoalSelected;

    private readonly BoxContainer _goalsContainer;
    private readonly RichTextLabel _currentGoalLabel;

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
            PanelOverride = new StyleBoxFlat
            {
                BackgroundColor = Color.FromHex("#20252f")
            },
            MinHeight = 60
        };

        var headerText = new RichTextLabel
        {
            Margin = new Thickness(8)
        };

        headerText.SetMarkup(
            "[font size=16][color=#5da9ff]ЦЕНТРАЛЬНОЕ КОМАНДОВАНИЕ[/color][/font]\n" +
            "Панель управления промышленными контрактами"
        );

        header.AddChild(headerText);

        root.AddChild(header);

        _currentGoalLabel = new RichTextLabel
        {
            Margin = new Thickness(4)
        };

        _currentGoalLabel.SetMarkup(
            "[color=#aaaaaa]Текущий контракт отсутствует[/color]");

        root.AddChild(_currentGoalLabel);

        var availableLabel = new Label
        {
            Text = "Доступные контракты:",
            FontColorOverride = Color.FromHex("#8fb8ff")
        };

        root.AddChild(availableLabel);

        var scroll = new ScrollContainer
        {
            VerticalExpand = true
        };

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
        _goalsContainer.RemoveAllChildren();

        if (state.CurrentGoal != null)
        {
            var color = state.GoalDifficulty switch
            {
                "Light" => "#5EFF7A",
                "Medium" => "#FFD95E",
                "Hard" => "#FF6B6B",
                _ => "#FFFFFF"
            };

            _currentGoalLabel.SetMarkup(
                $"[font size=14][color={color}]АКТИВНЫЙ КОНТРАКТ[/color][/font]\n" +
                $"Контракт: [bold]{state.GoalName ?? state.CurrentGoal}[/bold]\n" +
                $"Приоритет: {state.GoalDifficulty}\n" +
                $"Прогресс поставок: [color=#5da9ff]{state.CurrentProgress}/{state.RequiredAmount}[/color]"
            );
        }
        else
        {
            _currentGoalLabel.SetMarkup(
                "[color=#aaaaaa]Текущий контракт отсутствует[/color]");
        }

        foreach (var goalId in state.AvailableGoals)
        {
            var panel = new PanelContainer
            {
                PanelOverride = new StyleBoxFlat
                {
                    BackgroundColor = Color.FromHex("#252b36")
                }
            };

            var container = new BoxContainer
            {
                Orientation = BoxContainer.LayoutOrientation.Horizontal,
                SeparationOverride = 8,
                Margin = new Thickness(6)
            };

            var label = new Label
            {
                Text = goalId,
                HorizontalExpand = true
            };

            var button = new Button
            {
                Text = "Принять",
                MinWidth = 100
            };

            button.OnPressed += _ =>
            {
                OnGoalSelected?.Invoke(goalId);
            };

            container.AddChild(label);
            container.AddChild(button);

            panel.AddChild(container);

            _goalsContainer.AddChild(panel);
        }
    }
}
