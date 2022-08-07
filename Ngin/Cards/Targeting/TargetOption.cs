using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ngin.Cards.Targeting;

public class TargetOption<T> where T : ITargetable
{
    public ReadOnlyCollection<T> Targets => targets.AsReadOnly();

    private List<T> targets;

    public TargetOption(List<T> targets)
    {
        this.targets = targets;
    }
}