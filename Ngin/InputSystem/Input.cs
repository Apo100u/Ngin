using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ngin.Cards;
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

    public void AllowPass(Action onPass)
    {
        PassAction passAction = new(onPass);
        allowedActions.Add(passAction);
    }

    public void AllowChoosingCardFromSet(CardSet cardSet, Action<Card> onCardChosen)
    {
        for (int i = 0; i < cardSet.Count; i++)
        {
            CardChoiceAction cardChoiceAction = new(cardSet[i], onCardChosen);
            allowedActions.Add(cardChoiceAction);
        }
    }
}