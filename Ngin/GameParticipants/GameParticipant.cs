using Ngin.Characters;

namespace Ngin.GameParticipants;

public class GameParticipant
{
    public readonly string Name;
    public readonly Character[] OwnedCharacters;

    public GameParticipant(string name, params Character[] ownedCharacters)
    {
        Name = name;
        OwnedCharacters = ownedCharacters;

        for (int i = 0; i < OwnedCharacters.Length; i++)
        {
            ownedCharacters[i].SetOwner(this);
        }
    }

    public bool IsEveryCharacterDead()
    {
        bool allCharactersDead = true;

        for (int i = 0; i < OwnedCharacters.Length; i++)
        {
            if (!OwnedCharacters[i].IsDead)
            {
                allCharactersDead = false;
                break;
            }
        }

        return allCharactersDead;
    }
}