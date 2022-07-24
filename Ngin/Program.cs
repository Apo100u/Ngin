using System;
using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.InputSystem;
using Ngin.InputSystem.Actions;
using Ngin.LogSystem;

namespace Ngin;

internal class Program
{
    public static void Main(string[] args)
    {
        // TEST ===========================================================================
        System.Random random = new();
        Character character1 = new("character1", 10, random.Next(-3, 3));
        Character character2 = new("character2", 10, random.Next(-3, 3));
        Character character3 = new("character3", 10, random.Next(-3, 3));
        Character character4 = new("character4", 10, random.Next(-3, 3));
        Character character5 = new("character5", 10, random.Next(-3, 3));
        Character character6 = new("character6", 10, random.Next(-3, 3));
        Character character7 = new("character7", 10, random.Next(-3, 3));
        Character character8 = new("character8", 10, random.Next(-3, 3));
        Character character9 = new("character9", 10, random.Next(-3, 3));
        Character character10 = new("character10", 10, random.Next(-3, 3));

        Team team1 = new(character1, character2, character3, character4, character5);
        Team team2 = new(character6, character7, character8, character9, character10);

        ILog log = new ConsoleLog();
        GameSettings settings = new(
            cardsAllowedToPlayPerTurn: 1,
            cardsToDrawOnGameStart:    4,
            cardsToDrawOnTurnStart:    1,
            maxCardsInHand:            10);

        Game game = new(log, settings, team1, team2);
        game.Start();
        
        while (!game.IsFinished)
        {
            ShowAllowedActions();
            bool isInputValid = TryGetValidUserInput(out int chosenAction);

            if (isInputValid)
            {
                Game.Input.AllowedActions[chosenAction].Execute();
            }
            else
            {
                Console.WriteLine("Invalid input, try again.");
            }
        }

        // TEST ===========================================================================
    }

    private static void ShowAllowedActions()
    {
        for (int i = 0; i < Game.Input.AllowedActions.Count; i++)
        {
            string actionConsoleDescription = GetGameActionDescription(Game.Input.AllowedActions[i]);
            Console.WriteLine($"{i} - {actionConsoleDescription}");
        }
    }

    private static bool TryGetValidUserInput(out int chosenAction)
    {
        string userInput = Console.ReadLine();
        bool inputIsValid = int.TryParse(userInput, out chosenAction) && chosenAction >= 0 && chosenAction < Game.Input.AllowedActions.Count;
        return inputIsValid;
    }

    private static string GetGameActionDescription(GameAction gameAction)
    {
        return gameAction switch
        {
            PassAction passAction => "Pass",
            CardChoiceAction cardChoiceAction => $"Choose card {cardChoiceAction.Card.Name}",
            _ => $"ERROR: Description of GameAction of type \"{gameAction.GetType()}\" is unknown. It should be added to {nameof(GetGameActionDescription)}"
        };
    }
}