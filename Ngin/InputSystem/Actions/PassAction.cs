using System;

namespace Ngin.InputSystem.Actions;

public class PassAction : GameAction
{
    private Action onPass;

    public PassAction(Action onPass)
    {
        this.onPass = onPass;
    }

    public override void Execute()
    {
        onPass?.Invoke();
    }
}