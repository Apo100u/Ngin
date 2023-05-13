using System.Collections.Generic;
using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.InputSystem.Actions;

namespace Ngin.GameParticipants.AI;

public class RandomAi : GameParticipant
{
    public RandomAi(Game game, string name, params Character[] ownedCharacters) : base(game, name, ownedCharacters)
    {
    }

    public override void ChooseAction()
    {
        List<GameAction> consideredActions = new();

        for (int i = 0; i < Game.Input.AllowedActions.Count; i++)
        {
            if (Game.Input.AllowedActions[i] is not CancelAction)
            {
                consideredActions.Add(Game.Input.AllowedActions[i]);
            }
        }

        int randomizedIndex = RNG.NextInt(0, consideredActions.Count);
        
        consideredActions[randomizedIndex].Execute();
    }
}