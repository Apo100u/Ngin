using System;

namespace Ngin.InputSystem.Actions;

public class PassAction : GameAction
{
    private Action onPassed;

    public PassAction(Action onPassed)
    {
        this.onPassed = onPassed;
    }

    public override void Execute()
    {
        onPassed?.Invoke();
    }
}