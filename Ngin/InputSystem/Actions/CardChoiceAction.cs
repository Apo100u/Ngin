using System;
using Ngin.Cards;

namespace Ngin.InputSystem.Actions;

public class CardChoiceAction : GameAction
{
    public readonly Card Card;
    private Action<Card> onCardChosen;
    
    public CardChoiceAction(Card card, Action<Card> onCardChosen)
    {
        Card = card;
        this.onCardChosen = onCardChosen;
    }

    public override void Execute()
    {
        onCardChosen?.Invoke(Card);
    }
}