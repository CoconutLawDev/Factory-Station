using Content.Shared.FactoryStation;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client.FactoryStation.UI;

[UsedImplicitly]
public sealed class EnterpriseConsoleBoundUserInterface : BoundUserInterface
{
    private EnterpriseConsoleWindow? _window;

    public EnterpriseConsoleBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey) { }

    protected override void Open()
    {
        base.Open();
        _window = new EnterpriseConsoleWindow();
        _window.OpenCentered();
        _window.OnClose += Close;
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);
        if (state is EnterpriseConsoleBuiState consoleState)
        {
            _window?.UpdateMachines(consoleState.Machines);
        }
    }
}
