namespace Ngin.Characters;

public class Team
{
    public readonly Character[] Characters;

    public Team(params Character[] characters)
    {
        Characters = characters;

        for (int i = 0; i < Characters.Length; i++)
        {
            characters[i].SetTeam(this);
        }
    }
}