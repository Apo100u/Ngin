using Ngin.Characters;
using Ngin.Gameplay;

namespace Ngin.GameParticipants.AI.GOAP;

public class DrawGoal : Goal
{
    public override int GetPriority(Game game, GameParticipant gameParticipant)
    {
        int priority = 0;

        for (int i = 0; i < game.AllCharacters.Count; i++)
        {
            Character character = game.AllCharacters[i];
            bool isOwnedByParticipant = character.Owner == gameParticipant;

            if (isOwnedByParticipant && !character.IsDead)
            {
                int missingCards = Game.Settings.MaxCardsInHand - character.Hand.Count;
                priority += missingCards;
            }
        }

        return priority;
    }

    public override int GetScore(Game game, GameParticipant gameParticipant)
    {
        return Heuristics.GetScoreBasedOnCardsInHandCount(game, gameParticipant);
    }
}