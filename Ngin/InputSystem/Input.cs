using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ngin.Cards;
using Ngin.Cards.Targeting;
using Ngin.GameParticipants;
using Ngin.InputSystem.Actions;

namespace Ngin.InputSystem;

public class Input
{
    public GameParticipant ParticipantOnMove { get; private set; }
    
    public ReadOnlyCollection<GameAction> AllowedActions => allowedActions.AsReadOnly();
    
    private List<GameAction> allowedActions = new();

    public void StartNewChoice(GameParticipant participantOnMove)
    {
        allowedActions.Clear();
        ParticipantOnMove = participantOnMove;
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

    public void AllowChoosingCardFromCollection(IEnumerable<Card> cardCollection, Action<Card> onCardChosen)
    {
        foreach (Card card in cardCollection)
        {
            CardChoiceAction cardChoiceAction = new(card, onCardChosen);
            allowedActions.Add(cardChoiceAction);
        }
    }

    public void AllowChoosingTargetsFromOptions<T>(List<TargetOption<T>> targetOptions, Action<TargetOption<T>> onTargetOptionChosen)
    {
        for (int i = 0; i < targetOptions.Count; i++)
        {
            TargetChoiceAction<T> targetChoiceAction = new(targetOptions[i], onTargetOptionChosen);
            allowedActions.Add(targetChoiceAction);
        }
    }
}