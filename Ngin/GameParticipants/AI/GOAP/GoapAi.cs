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

    public override void ChooseAction()
    {
        if (!AnyActionsQueued)
        {
            currentGoal = GetHighestPriorityGoal();
        }
        
        base.ChooseAction();
    }

    protected override int GetParticipantsScore(Game game, GameParticipant gameParticipant)
    {
        return currentGoal.GetScore(game, gameParticipant);
    }

    private Goal GetHighestPriorityGoal()
    {
        Goal highestPriorityGoal = null;
        float highestFoundPriority = float.MinValue;

        for (int i = 0; i < possibleGoals.Length; i++)
        {
            float priority = possibleGoals[i].GetPriority(Game, this);

            if (priority > highestFoundPriority)
            {
                highestPriorityGoal = possibleGoals[i];
                highestFoundPriority = priority;
            }
        }
        
        return highestPriorityGoal;
    }
}