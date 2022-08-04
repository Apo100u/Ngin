using System;

namespace Ngin.Cards.Effects;

public class Draw : CardEffect
{
    private readonly int amount;

    public Draw(Card card, int amount) : base(card)
    {
        this.amount = amount;
    }

    public override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void Perform(Action onPerformed, Action onCancelled)
    {
        throw new System.NotImplementedException();
    }
}