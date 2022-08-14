using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ngin.Cards;
using Ngin.Cards.Targeting;
using Ngin.InputSystem.Actions;

namespace Ngin.InputSystem;

public class Input
{
    public ReadOnlyCollection<GameAction> AllowedActions => allowedActions.AsReadOnly();
    
    private List<GameAction> allowedActions = new();

    public void ClearAllowedActions()
    {
        allowedActions.Clear();
    }

    public void AllowPassing(Action onPassed)
    {
        PassAction passAction = new(onPassed);
        allowedActions.Add(passAction);
    }

    public void AllowCanceling(Action onCancelled)
    {
        CancelAction cancelAction = new(onCancelled);
        allowedActions.Add(cancelAction);
    }

    public void AllowChoosingCardFromSet(CardSet cardSet, Action<Card> onCardChosen)
    {
        for (int i = 0; i < cardSet.Count; i++)
        {
            CardChoiceAction cardChoiceAction = new(cardSet[i], onCardChosen);
            allowedActions.Add(cardChoiceAction);
        }
    }

    public void AllowChoosingTargetsFromOptions<T>(List<TargetOption<T>> targetOptions, Action<TargetOption<T>> onTargetOptionChosen)
    {
        for (int i = 0; i < targetOptions.Count; i++)
        {
            TargetChoiceAction<T> targetChoiceAction = new(targetOptions[i], onTargetOptionChosen);
        }
    }
}