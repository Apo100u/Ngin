using System;

namespace Ngin.Cards.Effects;

public class DealDamage : CardEffect
{
    private readonly int power;

    public DealDamage(Card card, int power) : base(card)
    {
        this.power = power;
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