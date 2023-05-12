namespace Ngin.Cards.Targeting;

public class TargetOption<T>
{
    public T[] Targets;

    public TargetOption(params T[] targets)
    {
        Targets = targets;
    }
}