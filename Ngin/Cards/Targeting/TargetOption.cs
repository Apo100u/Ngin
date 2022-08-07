using System;
using System.Collections.ObjectModel;

namespace Ngin.Cards.Targeting;

public class TargetOption<T> where T : ITargetable
{
    public ReadOnlyCollection<T> Targets => Array.AsReadOnly(targets);

    private T[] targets;

    public TargetOption(params T[] targets)
    {
        this.targets = targets;
    }
}