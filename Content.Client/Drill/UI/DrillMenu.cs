using Content.Client.Administration.UI.CustomControls;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;

namespace Content.Client.Drill.UI;

public sealed class DrillMenu : DefaultWindow
{
    public event Action<bool>? OnToggle;

    private readonly Label _batteryLabel;

    public DrillMenu()
    {
        Title = "Бур NanoTrasen™ NT-2000";
        SetSize = new(400, 350);

        // Инициализируем _batteryLabel перед использованием (убираем предупреждение CS8618)
        _batteryLabel = new Label
        {
            Text = "Заряд батареи: 100%",
            HorizontalAlignment = HAlignment.Center,
            Margin = new Thickness(0, 5)
        };

        var container = new BoxContainer
        {
            Orientation = BoxContainer.LayoutOrientation.Vertical,
            Margin = new Thickness(10)
        };

        // Заголовок
        container.AddChild(new Label
        {
            Text = "Буровая установка NT-2000",
            FontColorOverride = Color.FromHex("#ff9900")
        });

        // Инструкция
        container.AddChild(new Label
        {
            Text = "ОФИЦИАЛЬНАЯ ИНСТРУКЦИЯ\n\n" +
                   "Уважаемый сотрудник NanoTrasen!\n\n" +
                   "Буровая установка NT-2000 предназначена для добычи полезных " +
                   "ископаемых на астероидах и планетарных поверхностях.\n\n" +
                   "ПОРЯДОК РАБОТЫ:\n" +
                   "1. Разместите бур непосредственно на клетке с ресурсами.\n" +
                   "2. Нажмите кнопку 'ВКЛЮЧИТЬ' для запуска.\n" +
                   "3. Бур автоматически начнёт добычу ресурсов.\n" +
                   "4. При разряде батареи бур отключится.\n\n" +
                   "ВНИМАНИЕ!\n" +
                   "- Не прикасайтесь к работающему буру!\n" +
                   "- Используйте только одобренные NanoTrasen батареи.\n" +
                   "- Не оставляйте без присмотра.\n" +
                   "- При обнаружении неисправностей обратитесь к инженеру.\n\n" +
                   "NanoTrasen не несёт ответственности за травмы, " +
                   "полученные при использовании буровой установки. Соблюдайте технику безопасности.",
            MaxWidth = 380
        });

        // Разделитель
        container.AddChild(new HSeparator());

        // Индикатор заряда
        container.AddChild(_batteryLabel);

        // Кнопка включения/выключения
        var button = new Button
        {
            Text = "ВКЛЮЧИТЬ",
            ToggleMode = true,
            MinHeight = 40,
            Margin = new Thickness(0, 10)
        };
        button.OnToggled += args =>
        {
            button.Text = args.Pressed ? "ВЫКЛЮЧИТЬ" : "ВКЛЮЧИТЬ";
            OnToggle?.Invoke(args.Pressed);
        };
        container.AddChild(button);

        // Подпись под кнопкой
        container.AddChild(new Label
        {
            Text = "Нажмите для запуска/остановки бурения",
            HorizontalAlignment = HAlignment.Center,
            FontColorOverride = Color.FromHex("#888888")
        });

        ContentsContainer.AddChild(container);
    }

    public void UpdateBattery(float currentCharge, float maxCharge)
    {
        var percent = (int)(currentCharge / maxCharge * 100);
        _batteryLabel.Text = $"Заряд батареи: {percent}%";
    }
}
