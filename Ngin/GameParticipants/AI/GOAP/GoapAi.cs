using Ngin.Characters;
using Ngin.Gameplay;

namespace Ngin.GameParticipants.AI.GOAP;

public class GoapAi : TreeSearchAi
{
    private Goal currentGoal;
    private Goal[] possibleGoals;
    
    public GoapAi(Game game, string name, params Character[] ownedCharacters) : base(game, name, ownedCharacters)
    {
        possibleGoals = new Goal[]
        {
            new DamageGoal(),
            new HealGoal(),
            new DrawGoal()
        };
    }

    private Goal GetHighestPriorityGoal()
    {
        Goal highestPriorityGoal = null;
        float highestFoundPriority = float.MinValue;

        for (int i = 0; i < possibleGoals.Length; i++)
        {
            float priority = possibleGoals[i].GetPriority();

            if (priority > highestFoundPriority)
            {
                highestPriorityGoal = possibleGoals[i];
                highestFoundPriority = priority;
            }
        }
        
        return highestPriorityGoal;
    }
}