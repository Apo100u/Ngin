using System;
using Ngin.Cards.Targeting;

namespace Ngin.InputSystem.Actions;

public class TargetChoiceAction<T> : GameAction
{
    public readonly TargetOption<T> TargetOption;
    private Action<TargetOption<T>> onTargetOptionChosen;

    public TargetChoiceAction(TargetOption<T> targetOption, Action<TargetOption<T>> onTargetOptionChosen)
    {
        TargetOption = targetOption;
        this.onTargetOptionChosen = onTargetOptionChosen;
    }
    
    public override void Execute()
    {
        onTargetOptionChosen?.Invoke(TargetOption);
    }
}