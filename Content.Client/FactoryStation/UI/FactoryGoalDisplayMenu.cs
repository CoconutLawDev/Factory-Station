using System.Numerics;
using Content.Client.Message;
using Content.Shared.FactoryStation.States;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Maths;

namespace Content.Client.FactoryStation.UI;

public sealed class FactoryGoalDisplayMenu : DefaultWindow
{
    private readonly RichTextLabel _goalInfo;
    private readonly ProgressBar _progress;

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
            PanelOverride = new StyleBoxFlat
            {
                BackgroundColor = Color.FromHex("#1f2430")
            }
        };

        _goalInfo = new RichTextLabel
        {
            Margin = new Thickness(10)
        };

        panel.AddChild(_goalInfo);

        root.AddChild(panel);

        _progress = new ProgressBar
        {
            MinHeight = 24
        };

        root.AddChild(_progress);

        ContentsContainer.AddChild(root);

        SetNoGoal();
    }

    private void SetNoGoal()
    {
        _goalInfo.SetMarkupPermissive(
            "[font size=16][color=#aaaaaa]АКТИВНЫХ КОНТРАКТОВ НЕТ[/color][/font]");

        _progress.MaxValue = 1;
        _progress.Value = 0;
    }

    public void UpdateState(FactoryGoalUpdateState state)
    {
        if (state.CurrentGoal == null)
        {
            SetNoGoal();
            return;
        }

        var color = state.GoalDifficulty switch
        {
            "Light" => "#5EFF7A",
            "Medium" => "#FFD95E",
            "Hard" => "#FF6B6B",
            _ => "#FFFFFF"
        };

        _goalInfo.SetMarkupPermissive(
            $"[font size=16][color={color}]АКТИВНЫЙ ПРОМЫШЛЕННЫЙ КОНТРАКТ[/color][/font]\n\n" +
            $"Контракт: [bold]{state.GoalName ?? state.CurrentGoal}[/bold]\n" +
            $"Приоритет: {state.GoalDifficulty}\n" +
            $"Выполнение: {state.CurrentProgress}/{state.RequiredAmount}"
        );

        _progress.MaxValue = state.RequiredAmount;
        _progress.Value = state.CurrentProgress;
    }
}
