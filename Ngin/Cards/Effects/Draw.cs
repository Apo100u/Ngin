using System;
using Ngin.Characters;

namespace Ngin.Cards.Effects;

public class Draw : CardEffect
{
    private readonly int amount;

    public Draw(int amount)
    {
        this.amount = amount;
    }

    public override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void Perform(Character user, Action onPerformed, Action onCancelled)
    {
        throw new System.NotImplementedException();
    }
}