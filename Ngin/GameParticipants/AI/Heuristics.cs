using System.Linq;
using Ngin.Gameplay;

namespace Ngin.GameParticipants.AI;

public static class Heuristics
{
    public static int GetScoreBasedOnHealth(Game game, GameParticipant gameParticipant)
    {
        int deadCharacterValue = (int)game.AllCharacters.Select(x => x.Health.Base).Average();
        int score = 0;

        for (int i = 0; i < game.AllCharacters.Count; i++)
        {
            bool isOwnedByParticipant = game.AllCharacters[i].Owner == gameParticipant;
            
            if (game.AllCharacters[i].IsDead)
            {
                score += isOwnedByParticipant
                    ? -deadCharacterValue
                    : deadCharacterValue;
            }
            else
            {
                score += isOwnedByParticipant
                    ? game.AllCharacters[i].Health.Current
                    : -game.AllCharacters[i].Health.Current;
            }
        }
        
        return score;
    }
    
    public static int GetScoreBasedOnCardsInHandCount(Game game, GameParticipant gameParticipant)
    {
        int cardInHandValue = (int)game.AllCharacters.Select(x => x.Hand.Count).Average();;
        int score = 0;

        for (int i = 0; i < game.AllCharacters.Count; i++)
        {
            bool isOwnedByParticipant = game.AllCharacters[i].Owner == gameParticipant;
            
            if (isOwnedByParticipant && !game.AllCharacters[i].IsDead)
            {
                score += cardInHandValue * game.AllCharacters[i].Hand.Count;
            }
        }
        
        return score;
    }
}