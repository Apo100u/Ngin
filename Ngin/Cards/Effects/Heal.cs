using System;

namespace Ngin.Cards.Effects;

public class Heal : CardEffect
{
    private readonly int power;

    public Heal(int power)
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