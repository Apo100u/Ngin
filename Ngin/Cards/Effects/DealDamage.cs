using System;
using System.Collections.Generic;
using Ngin.Cards.Targeting;
using Ngin.Characters;

namespace Ngin.Cards.Effects;

public class DealDamage : CardEffect
{
    private readonly int power;

    private CharacterTargetingType targetingType;

    public DealDamage(int power, CharacterTargetingType targetingType)
    {
        this.power = power;
        this.targetingType = targetingType;
    }

    public override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void Perform(Character user, Action onPerformed, Action onCancelled)
    {
        List<TargetOption<Character>> targetsSets = targetingType.GetAvailableTargetOptions(user);
        
        // TODO: Choose target option through input if needed, then execute the effect on chosen target.
        // Consider automatically executing effect if there is only one target
    }
}