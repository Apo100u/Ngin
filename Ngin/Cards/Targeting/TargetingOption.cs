using System;
using System.Collections.Generic;

namespace Ngin.Cards.Targeting;

public sealed class TargetingOption
{
    private Func<List<ICardEffectTarget>> targetsGettingAction;

    private TargetingOption(Func<List<ICardEffectTarget>> targetsGettingAction)
    {
        this.targetsGettingAction = targetsGettingAction;
    }

    public List<ICardEffectTarget> GetAvailableTargets()
    {
        List<ICardEffectTarget> availableTargets = targetsGettingAction();
        return availableTargets;
    }
}