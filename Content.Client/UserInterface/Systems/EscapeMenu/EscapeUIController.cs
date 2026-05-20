using Content.Client.FeedbackPopup;
using Content.Client.Gameplay;
using Content.Client.UserInterface.Controls;
using Content.Client.UserInterface.Systems.Guidebook;
using Content.Client.UserInterface.Systems.Info;
using Content.Shared.CCVar;
using JetBrains.Annotations;
using Robust.Client.Console;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controllers;
using Robust.Shared.Configuration;
using Robust.Shared.Input;
using Robust.Shared.Input.Binding;
using Robust.Shared.Utility;
using static Robust.Client.UserInterface.Controls.BaseButton;

namespace Content.Client.UserInterface.Systems.EscapeMenu;

[UsedImplicitly]
public sealed partial class EscapeUIController : UIController,
    IOnStateEntered<GameplayState>,
    IOnStateExited<GameplayState>
{
    [Dependency] private IClientConsoleHost _console = default!;
    [Dependency] private IUriOpener _uri = default!;
    [Dependency] private IConfigurationManager _cfg = default!;
    [Dependency] private ChangelogUIController _changelog = default!;
    [Dependency] private InfoUIController _info = default!;
    [Dependency] private OptionsUIController _options = default!;
    [Dependency] private GuidebookUIController _guidebook = default!;
    [Dependency] private FeedbackPopupUIController _feedback = default!;

    private Options.UI.EscapeMenu? _escapeWindow;

    private MenuButton? EscapeButton =>
        UIManager.GetActiveUIWidgetOrNull<MenuBar.Widgets.GameTopMenuBar>()
            ?.EscapeButton;

    public void UnloadButton()
    {
        if (EscapeButton == null)
            return;

        EscapeButton.Pressed = false;
        EscapeButton.OnPressed -= EscapeButtonOnOnPressed;
    }

    public void LoadButton()
    {
        if (EscapeButton == null)
            return;

        EscapeButton.OnPressed += EscapeButtonOnOnPressed;
    }

    private void ActivateButton()
    {
        EscapeButton?.SetClickPressed(true);
    }

    private void DeactivateButton()
    {
        EscapeButton?.SetClickPressed(false);
    }

    public void OnStateEntered(GameplayState state)
    {
        DebugTools.Assert(_escapeWindow == null);

        _escapeWindow = UIManager.CreateWindow<Options.UI.EscapeMenu>();

        _escapeWindow.OnClose += DeactivateButton;
        _escapeWindow.OnOpen += ActivateButton;

        // Feedback
        _escapeWindow.FeedbackButton.OnPressed += _ =>
        {
            CloseEscapeWindow();
            _feedback.ToggleWindow();
        };

        // Discord
        _escapeWindow.DiscordButton.OnPressed += _ =>
        {
            _uri.OpenUri("https://discord.gg/e2zwWhxRr8");
        };

        // Changelog
        _escapeWindow.ChangelogButton.OnPressed += _ =>
        {
            CloseEscapeWindow();
            _changelog.ToggleWindow();
        };

        // Rules
        _escapeWindow.RulesButton.OnPressed += _ =>
        {
            CloseEscapeWindow();
            _info.OpenWindow();
        };

        // Disconnect
        _escapeWindow.DisconnectButton.OnPressed += _ =>
        {
            CloseEscapeWindow();
            _console.ExecuteCommand("disconnect");
        };

        // Options
        _escapeWindow.OptionsButton.OnPressed += _ =>
        {
            CloseEscapeWindow();
            _options.OpenWindow();
        };

        // Quit
        _escapeWindow.QuitButton.OnPressed += _ =>
        {
            CloseEscapeWindow();
            _console.ExecuteCommand("quit");
        };

        // Wiki
        _escapeWindow.WikiButton.OnPressed += _ =>
        {
            _uri.OpenUri(_cfg.GetCVar(CCVars.InfoLinksWiki));
        };

        // Guidebook
        _escapeWindow.GuidebookButton.OnPressed += _ =>
        {
            _guidebook.ToggleGuidebook();
        };

        // Hide wiki button if link is empty.
        _escapeWindow.WikiButton.Visible =
            _cfg.GetCVar(CCVars.InfoLinksWiki) != "";

        CommandBinds.Builder
            .Bind(
                EngineKeyFunctions.EscapeMenu,
                InputCmdHandler.FromDelegate(_ => ToggleWindow()))
            .Register<EscapeUIController>();
    }

    public void OnStateExited(GameplayState state)
    {
        if (_escapeWindow != null)
        {
            _escapeWindow.Dispose();
            _escapeWindow = null;
        }

        CommandBinds.Unregister<EscapeUIController>();
    }

    private void EscapeButtonOnOnPressed(ButtonEventArgs args)
    {
        ToggleWindow();
    }

    private void CloseEscapeWindow()
    {
        _escapeWindow?.Close();
    }

    /// <summary>
    /// Toggles the game menu.
    /// </summary>
    public void ToggleWindow()
    {
        if (_escapeWindow == null)
            return;

        if (_escapeWindow.IsOpen)
        {
            CloseEscapeWindow();

            if (EscapeButton != null)
                EscapeButton.Pressed = false;
        }
        else
        {
            _escapeWindow.OpenCentered();

            if (EscapeButton != null)
                EscapeButton.Pressed = true;
        }
    }
}
