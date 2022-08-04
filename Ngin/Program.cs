using System;
using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.InputSystem.Actions;
using Ngin.LogSystem;

namespace Ngin;

internal class Program
{
    public static void Main(string[] args)
    {
        // TEST ===========================================================================
        
        ILog log = new ConsoleLog();
        GameSettings settings = new(
            cardsAllowedToPlayPerTurn: 1,
            cardsToDrawOnGameStart:    4,
            cardsToDrawOnTurnStart:    1,
            maxCardsInHand:            10);
        
        Game game = new(log, settings);
        
        Random random = new();
        Character character1  = new(game, "Character1", 10, random.Next(-3, 3));
        Character character2  = new(game, "Character2", 10, random.Next(-3, 3));
        Character character3  = new(game, "Character3", 10, random.Next(-3, 3));
        Character character4  = new(game, "Character4", 10, random.Next(-3, 3));
        Character character5  = new(game, "Character5", 10, random.Next(-3, 3));
        Character character6  = new(game, "Character6", 10, random.Next(-3, 3));
        Character character7  = new(game, "Character7", 10, random.Next(-3, 3));
        Character character8  = new(game, "Character8", 10, random.Next(-3, 3));
        Character character9  = new(game, "Character9", 10, random.Next(-3, 3));
        Character character10 = new(game, "Character10", 10, random.Next(-3, 3));

        Team team1 = new(character1, character2, character3, character4, character5);
        Team team2 = new(character6, character7, character8, character9, character10);
        
        game.SetTeams(team1, team2);
        game.Start();
        
        // TEST ===========================================================================
        
        while (!game.IsFinished)
        {
            ShowAllowedActions(game);
            bool isInputValid = TryGetValidUserInput(game, out int chosenAction);

            if (isInputValid)
            {
                game.Input.AllowedActions[chosenAction].Execute();
            }
            else
            {
                Console.WriteLine("Invalid input, try again.");
            }
        }

    }

    private static void ShowAllowedActions(Game game)
    {
        for (int i = 0; i < game.Input.AllowedActions.Count; i++)
        {
            string actionConsoleDescription = GetGameActionDescription(game.Input.AllowedActions[i]);
            Console.WriteLine($"{i} - {actionConsoleDescription}");
        }
    }

    private static bool TryGetValidUserInput(Game game, out int chosenAction)
    {
        string userInput = Console.ReadLine();
        bool inputIsValid = int.TryParse(userInput, out chosenAction) && chosenAction >= 0 && chosenAction < game.Input.AllowedActions.Count;
        return inputIsValid;
    }

    private static string GetGameActionDescription(GameAction gameAction)
    {
        return gameAction switch
        {
            PassAction passAction => "Pass",
            CardChoiceAction cardChoiceAction => $"Choose card {cardChoiceAction.Card.Name}",
            _ => $"ERROR: Description of GameAction of type \"{gameAction.GetType()}\" is unknown. It should be added to {nameof(GetGameActionDescription)}."
        };
    }
}