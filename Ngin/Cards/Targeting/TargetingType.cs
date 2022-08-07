using System.Collections.Generic;
using Ngin.Characters;

namespace Ngin.Cards.Targeting;

public abstract class TargetingType<T> where T : ITargetable
{
    protected delegate List<TargetOption<T>> TargetsFinder(Character user);

    private TargetsFinder targetsFinder;

    protected TargetingType(TargetsFinder targetsFinder)
    {
        this.targetsFinder = targetsFinder;
    }

    public List<TargetOption<T>> GetAvailableTargetOptions(Character user)
    {
        return targetsFinder(user);
    }
}