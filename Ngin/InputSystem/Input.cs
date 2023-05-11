using System;
using System.Collections.Generic;
using Ngin.Cards;
using Ngin.Cards.Targeting;
using Ngin.GameParticipants;
using Ngin.Gameplay;
using Ngin.InputSystem.Actions;

namespace Ngin.InputSystem;

public abstract class Input
{
    public GameParticipant ParticipantOnMove { get; private set; }

    protected Game Game;
    protected List<GameAction> AllowedActions = new();

    protected Input(Game game)
    {
        Game = game;
    }

    public abstract GameAction ReadUserActionChoice();
    
    public void StartNewChoice(GameParticipant participantOnMove)
    {
        AllowedActions.Clear();
        ParticipantOnMove = participantOnMove;
    }

    public void AllowPassing(Action onPassed)
    {
        PassAction passAction = new(onPassed);
        AllowedActions.Add(passAction);
    }

    public void AllowCanceling(Action onCancelled)
    {
        CancelAction cancelAction = new(onCancelled);
        AllowedActions.Add(cancelAction);
    }

    public void AllowChoosingCardFromCollection(IEnumerable<Card> cardCollection, Action<Card> onCardChosen)
    {
        foreach (Card card in cardCollection)
        {
            CardChoiceAction cardChoiceAction = new(card, onCardChosen);
            AllowedActions.Add(cardChoiceAction);
        }
    }

    public void AllowChoosingTargetsFromOptions<T>(List<TargetOption<T>> targetOptions, Action<TargetOption<T>> onTargetOptionChosen)
    {
        for (int i = 0; i < targetOptions.Count; i++)
        {
            TargetChoiceAction<T> targetChoiceAction = new(targetOptions[i], onTargetOptionChosen);
            AllowedActions.Add(targetChoiceAction);
        }
    }
}