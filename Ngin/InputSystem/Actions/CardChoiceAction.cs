using System;
using Ngin.Cards;

namespace Ngin.InputSystem.Actions;

public class CardChoiceAction : GameAction
{
    private Card card;
    private Action<Card> onCardChosen;
    
    public CardChoiceAction(Card card, Action<Card> onCardChosen)
    {
        this.card = card;
        this.onCardChosen = onCardChosen;
    }

    public override void Execute()
    {
        onCardChosen?.Invoke(card);
    }
}