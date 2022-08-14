using System;

namespace Ngin.InputSystem.Actions;

public class CancelAction : GameAction
{
    private Action onCancel;
    
    public CancelAction(Action onCancel)
    {
        this.onCancel = onCancel;
    }
    
    public override void Execute()
    {
        onCancel?.Invoke();
    }
}