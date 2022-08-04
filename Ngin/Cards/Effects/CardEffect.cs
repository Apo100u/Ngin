using System;

namespace Ngin.Cards.Effects;

public abstract class CardEffect
{
    protected readonly Card Card;
    
    protected CardEffect(Card card)
    {
        Card = card;
    }
    
    public abstract string GetDescription();
    public abstract void Perform(Action onPerformed, Action onCancelled);
}