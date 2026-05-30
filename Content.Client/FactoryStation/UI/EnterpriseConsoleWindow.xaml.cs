using System.Linq;
using System.Numerics;
using Content.Client.UserInterface.Controls;
using Content.Shared.FactoryStation;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;

namespace Content.Client.FactoryStation.UI;

public sealed class EnterpriseConsoleWindow : DefaultWindow
{
    private readonly BoxContainer _machineList;

    public EnterpriseConsoleWindow()
    {
        Title = "Консоль предприятия";
        SetSize = new Vector2(600, 400);

        var scroll = new ScrollContainer
        {
            VerticalExpand = true,
            HScrollEnabled = false
        };

        _machineList = new BoxContainer
        {
            Orientation = BoxContainer.LayoutOrientation.Vertical,
            Margin = new Thickness(8)
        };

        scroll.AddChild(_machineList);
        Contents.AddChild(scroll);
    }

    public void UpdateMachines(List<MachineInfo> machines)
    {
        _machineList.RemoveAllChildren();
        foreach (var machine in machines)
        {
            var panel = new PanelContainer
            {
                StyleClasses = { "PanelDark" },
                Margin = new Thickness(0, 2),
            };

            var container = new BoxContainer
            {
                Orientation = BoxContainer.LayoutOrientation.Vertical,
                Margin = new Thickness(6),
            };

            // Цветной индикатор статуса
            var title = new RichTextLabel();
            string color = machine.Status switch
            {
                MachineStatus.Normal => "#00FF00",
                MachineStatus.Warning => "#FFFF00",
                MachineStatus.Critical => "#FF0000",
                MachineStatus.Offline => "#888888",
                _ => "#FFFFFF"
            };
            title.SetMessage($"[color={color}]\uf111[/color] {machine.Name}");

            container.AddChild(title);

            if (!string.IsNullOrEmpty(machine.ActiveRecipe))
                container.AddChild(new Label { Text = $"Рецепт: {machine.ActiveRecipe}" });

            if (machine.Materials.Count > 0)
            {
                var mats = string.Join(", ", machine.Materials.Select(m => $"{m.Key}: {m.Value}"));
                container.AddChild(new Label { Text = $"Материалы: {mats}" });
            }

            if (machine.Temperature.HasValue)
                container.AddChild(new Label { Text = $"Температура: {machine.Temperature.Value:F1}°C" });

            container.AddChild(new Label { Text = $"Целостность: {100 - machine.Damage}%" });

            string statusText = machine.Status switch
            {
                MachineStatus.Normal => "Норма",
                MachineStatus.Warning => "Предупреждение",
                MachineStatus.Critical => "КРИТИЧЕСКОЕ - ПРЕДУПРЕЖДЕНИЕ!",
                MachineStatus.Offline => "Нет питания",
                _ => "Неизвестно"
            };
            container.AddChild(new Label { Text = $"Статус: {statusText}" });

            panel.AddChild(container);
            _machineList.AddChild(panel);
        }
    }
}
