namespace Ngin.Characters;

public class Statistic
{
    public int Base { get; }
    public int Current { get; private set; }

    public Statistic(int @base)
    {
        Base = @base;
        Current = Base;
    }

    public void ChangeBy(int amount)
    {
        Current += amount;
    }
}