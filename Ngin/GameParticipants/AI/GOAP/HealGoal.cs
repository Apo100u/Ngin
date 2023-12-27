using Ngin.Characters;
using Ngin.Gameplay;

namespace Ngin.GameParticipants.AI.GOAP;

public class HealGoal : Goal
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
                int missingHealth = character.Health.Base - character.Health.Current;
                priority += missingHealth * missingHealth;
            }
        }

        return priority;
    }

    public override int GetScore(Game game, GameParticipant gameParticipant)
    {
        return Heuristics.GetScoreBasedOnHealth(game, gameParticipant);
    }
}