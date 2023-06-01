using System.Linq;
using Ngin.Gameplay;

namespace Ngin.GameParticipants.AI;

public static class Heuristics
{
    public static int GetParticipantsScoreBasedOnHealth(Game game, GameParticipant gameParticipant)
    {
        int deadCharacterValue = (int)game.AllCharacters.Select(x => x.Health.Base).Average();
        int participantScore = 0;

        for (int i = 0; i < game.AllCharacters.Count; i++)
        {
            bool isOwnedByParticipant = game.AllCharacters[i].Owner == gameParticipant;
            
            if (game.AllCharacters[i].IsDead)
            {
                participantScore += isOwnedByParticipant
                    ? -deadCharacterValue
                    : deadCharacterValue;
            }
            else
            {
                participantScore += isOwnedByParticipant
                    ? game.AllCharacters[i].Health.Current
                    : -game.AllCharacters[i].Health.Current;
            }
        }
        
        return participantScore;
    }
}