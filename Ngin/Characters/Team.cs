namespace Ngin.Characters;

public class Team
{
    public readonly Character[] Characters;

    public Team(params Character[] characters)
    {
        Characters = characters;
    }
}