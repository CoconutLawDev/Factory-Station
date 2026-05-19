using System;
using Content.Shared.FactoryStation.States;
using Robust.Client.UserInterface;

namespace Content.Client.FactoryStation.UI;

public sealed class FactoryGoalDisplayBoundUserInterface : BoundUserInterface
{
    private FactoryGoalDisplayMenu? _menu;

    public FactoryGoalDisplayBoundUserInterface(EntityUid owner, Enum uiKey)
        : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        // Не создаём повторно существующее окно.
        if (_menu != null)
            return;

        _menu = new FactoryGoalDisplayMenu();

        _menu.OnClose += OnMenuClosed;

        _menu.OpenCentered();
    }

    private void OnMenuClosed()
    {
        if (_menu != null)
            _menu.OnClose -= OnMenuClosed;

        _menu = null;
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (state is not FactoryGoalUpdateState goalState)
            return;

        _menu?.UpdateState(goalState);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (!disposing)
            return;

        if (_menu != null)
        {
            _menu.OnClose -= OnMenuClosed;
            _menu.Close();
            _menu = null;
        }
    }
}
