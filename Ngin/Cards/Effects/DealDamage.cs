using System;
using System.Collections.Generic;
using Ngin.Cards.Targeting;
using Ngin.Characters;

namespace Ngin.Cards.Effects;

public class DealDamage : CardEffect
{
    private readonly int power;

    private CharacterTargetingType targetingType;

    public DealDamage(int power)
    {
        this.power = power;
    }

    public override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void Perform(Character user, Action onPerformed, Action onCancelled)
    {
        List<TargetOption<Character>> targetsSets = targetingType.GetAvailableTargetOptions(user);
    }
}