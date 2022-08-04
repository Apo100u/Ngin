using System;

namespace Ngin.Cards.Effects;

public abstract class CardEffect
{
    public abstract string GetDescription();
    public abstract void Perform(Action onPerformed, Action onCancelled);
}