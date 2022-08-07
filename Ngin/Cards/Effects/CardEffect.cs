using System;
using Ngin.Characters;

namespace Ngin.Cards.Effects;

public abstract class CardEffect
{
    public abstract string GetDescription();
    public abstract void Perform(Character user, Action onPerformed, Action onCancelled);
}