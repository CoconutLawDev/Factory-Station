using Robust.Client.Graphics;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Maths;
using static Robust.Client.UserInterface.StylesheetHelpers;

namespace Content.Client.FactoryStation.StyleSheets;

public sealed class FactoryStylesheet
{
    public Stylesheet Stylesheet { get; }

    public FactoryStylesheet(IResourceCache res, IUserInterfaceManager userInterfaceManager)
    {
        // Используем тот же кириллический шрифт, что и в DefaultStylesheet (NotoSans)
        var notoSansFont = res.GetResource<FontResource>("/EngineFonts/NotoSans/NotoSans-Regular.ttf");
        var notoSansFont12 = new VectorFont(notoSansFont, 12);

        var theme = userInterfaceManager.CurrentTheme;

        // Определим цвета
        var darkMetal = Color.FromHex("#2A2A2E");
        var orange = Color.FromHex("#FF8C00");
        var white = Color.White;
        var black = Color.Black;
        var gray = Color.FromHex("#808080");

        Stylesheet = new Stylesheet(new StyleRule[]
        {
            // ===== ОКНА =====
            // Панель окна (основной фон)
            Element().Class(DefaultWindow.StyleClassWindowPanel)
                .Prop("panel", new StyleBoxFlat
                {
                    BackgroundColor = darkMetal,
                    BorderColor = orange,
                    BorderThickness = new Thickness(1),
                }),

            // Заголовок окна
            Element().Class(DefaultWindow.StyleClassWindowHeader)
                .Prop(PanelContainer.StylePropertyPanel, new StyleBoxFlat
                {
                    BackgroundColor = orange,
                    BorderColor = orange,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Padding = new Thickness(1),
                }),

            // ===== ШРИФТЫ =====
            // Основной шрифт и цвет
            Element()
                .Prop("font", notoSansFont12)
                .Prop("font-color", white),

            // ===== КНОПКИ =====
            // Кнопка обычная
            Element<ContainerButton>().Class(ContainerButton.StyleClassButton).Pseudo(ContainerButton.StylePseudoClassNormal)
                .Prop(ContainerButton.StylePropertyStyleBox, new StyleBoxFlat
                {
                    BackgroundColor = darkMetal,
                    BorderThickness = new Thickness(1),
                    BorderColor = orange,
                    Padding = new Thickness(3),
                    ContentMarginBottomOverride = 3,
                    ContentMarginLeftOverride = 5,
                    ContentMarginRightOverride = 5,
                    ContentMarginTopOverride = 3,
                }),

            // Кнопка при наведении
            Element<ContainerButton>().Class(ContainerButton.StyleClassButton).Pseudo(ContainerButton.StylePseudoClassHover)
                .Prop(ContainerButton.StylePropertyStyleBox, new StyleBoxFlat
                {
                    BackgroundColor = orange,
                    BorderThickness = new Thickness(1),
                    BorderColor = orange,
                    Padding = new Thickness(3),
                    ContentMarginBottomOverride = 3,
                    ContentMarginLeftOverride = 5,
                    ContentMarginRightOverride = 5,
                    ContentMarginTopOverride = 3,
                }),

            // Кнопка нажатая
            Element<ContainerButton>().Class(ContainerButton.StyleClassButton).Pseudo(ContainerButton.StylePseudoClassPressed)
                .Prop(ContainerButton.StylePropertyStyleBox, new StyleBoxFlat
                {
                    BackgroundColor = Color.FromHex("#CC7000"),
                    BorderThickness = new Thickness(1),
                    BorderColor = orange,
                    Padding = new Thickness(3),
                    ContentMarginBottomOverride = 3,
                    ContentMarginLeftOverride = 5,
                    ContentMarginRightOverride = 5,
                    ContentMarginTopOverride = 3,
                }),

            // Кнопка неактивная
            Element<ContainerButton>().Class(ContainerButton.StyleClassButton).Pseudo(ContainerButton.StylePseudoClassDisabled)
                .Prop(ContainerButton.StylePropertyStyleBox, new StyleBoxFlat
                {
                    BackgroundColor = gray,
                    BorderThickness = new Thickness(1),
                    BorderColor = gray,
                    Padding = new Thickness(3),
                    ContentMarginBottomOverride = 3,
                    ContentMarginLeftOverride = 5,
                    ContentMarginRightOverride = 5,
                    ContentMarginTopOverride = 3,
                }),

            // ===== ПОЛЯ ВВОДА =====
            Element<LineEdit>()
                .Prop(LineEdit.StylePropertyStyleBox, new StyleBoxFlat
                {
                    BackgroundColor = darkMetal,
                    BorderThickness = new Thickness(1),
                    BorderColor = orange,
                    Padding = new Thickness(3),
                    ContentMarginBottomOverride = 3,
                    ContentMarginLeftOverride = 5,
                    ContentMarginRightOverride = 5,
                    ContentMarginTopOverride = 3,
                })
                .Prop("font-color", white)
                .Prop("cursor-color", white),

            Element<LineEdit>().Class(LineEdit.StyleClassLineEditNotEditable)
                .Prop("font-color", gray),

            // ===== ПРОГРЕСС-БАР (если используется, но можно добавить для стиля) =====
            // В движке ProgressBar может не иметь стилей, но если нужен – добавьте через собственные классы.
        });
    }
}
