using System.Numerics;
using Content.Client.Message;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;

namespace Content.Client.FactoryStation.UI;

public sealed class GoalConfirmationDialog : DefaultWindow
{
    public event Action? OnConfirmed;
    public event Action? OnCancelled;

    public GoalConfirmationDialog(string goalName, int currentProgress)
    {
        Title = "Подтверждение смены контракта";
        MinSize = SetSize = new Vector2(400, 200);

        var root = new BoxContainer
        {
            Orientation = BoxContainer.LayoutOrientation.Vertical,
            SeparationOverride = 12,
            Margin = new Thickness(10)
        };

        var message = new RichTextLabel();
        message.SetMarkup(
            $"Вы уверены, что хотите сменить промышленный контракт?\n\n" +
            $"Текущий прогресс ([color=#FFD95E]{currentProgress} ед.[/color]) будет [color=#FF6B6B]утерян[/color] и списан в пользу NanoTrasen как штраф за невыполнение.\n\n" +
            $"Новый контракт: [bold]{goalName}[/bold]");

        root.AddChild(message);

        var buttonContainer = new BoxContainer
        {
            Orientation = BoxContainer.LayoutOrientation.Horizontal,
            HorizontalAlignment = Control.HAlignment.Center,
            SeparationOverride = 10
        };

        var cancelButton = new Button
        {
            Text = "Отмена",
            MinWidth = 100
        };
        cancelButton.OnPressed += _ =>
        {
            OnCancelled?.Invoke();
            Close();
        };

        var confirmButton = new Button
        {
            Text = "Принять",
            MinWidth = 100,
            StyleClasses = { "Danger" }
        };
        confirmButton.OnPressed += _ =>
        {
            OnConfirmed?.Invoke();
            Close();
        };

        buttonContainer.AddChild(cancelButton);
        buttonContainer.AddChild(confirmButton);

        root.AddChild(buttonContainer);
        ContentsContainer.AddChild(root);
    }
}
