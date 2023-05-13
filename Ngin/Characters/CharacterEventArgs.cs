using Ngin.Cards;

namespace Ngin.Characters;

public class DamagedEventArgs
{
    public readonly Character Character;
    public readonly int ActualDamageTaken;

    public DamagedEventArgs(Character character, int actualDamageTaken)
    {
        Character = character;
        ActualDamageTaken = actualDamageTaken;
    }
}

public class HealedEventArgs
{
    public readonly Character Character;
    public readonly int ActualHealTaken;

    public HealedEventArgs(Character character, int actualHealTaken)
    {
        Character = character;
        ActualHealTaken = actualHealTaken;
    }
}

public class PlayedCardFromHandEventArgs
{
    public readonly Character Character;
    public readonly Card PlayedCard;

    public PlayedCardFromHandEventArgs(Character character, Card playedCard)
    {
        Character = character;
        PlayedCard = playedCard;
    }
}