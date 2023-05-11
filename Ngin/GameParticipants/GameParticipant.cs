using Ngin.Characters;
using Ngin.Gameplay;

namespace Ngin.GameParticipants;

public abstract class GameParticipant
{
    public readonly string Name;
    public readonly Character[] OwnedCharacters;

    protected Game Game;

    protected GameParticipant(Game game, string name, params Character[] ownedCharacters)
    {
        Game = game;
        Name = name;
        OwnedCharacters = ownedCharacters;

        for (int i = 0; i < OwnedCharacters.Length; i++)
        {
            ownedCharacters[i].SetOwner(this);
        }
    }

    public abstract void MakeMove();

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