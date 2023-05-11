using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.InputSystem.Actions;

namespace Ngin.GameParticipants;

public class HumanPlayer : GameParticipant
{
    public HumanPlayer(Game game, string name, params Character[] ownedCharacters) : base(game, name, ownedCharacters)
    {
    }

    public override void MakeMove()
    {
        GameAction chosenAction = Game.Input.ReadUserActionChoice();
        chosenAction.Execute();
    }
}