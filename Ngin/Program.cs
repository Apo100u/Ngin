using System;
using System.Collections.Generic;
using System.Linq;
using Ngin.Characters;
using Ngin.GameParticipants;
using Ngin.Gameplay;
using Ngin.Helpers.Cards;
using Ngin.InputSystem.Actions;
using Ngin.LogSystem;

namespace Ngin;

internal class Program
{
    public static void Main(string[] args)
    {
        // TEST ===========================================================================
        
        GameSettings settings = new(
            cardsAllowedToPlayPerTurn: 1,
            cardsToDrawOnGameStart:    2,
            cardsToDrawOnTurnStart:    1,
            maxCardsInHand:            5);
        
        Game game = new(settings);
        
        Random random = new();
        Character character1  = new(game, "Team1.Character1", 10, random.Next(-3, 3), CardsFactory.SimpleExampleDeck());
        Character character2  = new(game, "Team1.Character2", 10, random.Next(-3, 3), CardsFactory.SimpleExampleDeck());
        Character character3  = new(game, "Team2.Character3", 10, random.Next(-3, 3), CardsFactory.SimpleExampleDeck());
        Character character4  = new(game, "Team2.Character4", 10, random.Next(-3, 3), CardsFactory.SimpleExampleDeck());

        GameParticipant team1 = new("Participant 1", character1, character2);
        GameParticipant team2 = new("Participant 2", character3, character4);
        
        game.SetParticipants(team1, team2);
        
        ConsoleLog consoleLog = new();
        consoleLog.StartLogging(game);

        game.Start();
        
        // TEST ===========================================================================
        
        while (!game.IsFinished) // Previous game loop, DELETE if new game loop is working
        {
            ShowAllowedActions(game.Input.AllowedActions);
        
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
        
        // while (!game.IsFinished)
        // {
        //     ShowAllowedActions(game.Input.AllowedActions);
        //
        //     GameAction chosenAction = game.Input.ParticipantOnMove.ChooseAction(game.Input.AllowedActions);
        //     chosenAction.Execute();
        // }
    }

    private static void ShowAllowedActions(IEnumerable<GameAction> allowedActions) 
    {
        int gameActionNumber = 0;
        
        foreach (GameAction allowedAction in allowedActions)
        {
            string actionConsoleDescription = GetGameActionDescription(allowedAction);
            Console.WriteLine($"{gameActionNumber} - {actionConsoleDescription}");
            
            gameActionNumber++;
        }
    }

    private static bool TryGetValidUserInput(Game game, out int chosenAction)
    {
        string userInput = Console.ReadLine();
        bool isInputValid = int.TryParse(userInput, out chosenAction) && chosenAction >= 0 && chosenAction < game.Input.AllowedActions.Count;
        return isInputValid;
    }

    private static string GetGameActionDescription(GameAction gameAction)
    {
        return gameAction switch
        {
            PassAction pass => "Pass",
            CancelAction cancel => "Cancel",
            CardChoiceAction cardChoice => $"Choose card: {cardChoice.Card.Name}",
            TargetChoiceAction<Character> targetChoice => $"Choose target: {string.Join(", ", targetChoice.TargetOption.Targets.Select(x => x.Name))}",
            _ => $"ERROR: Description of GameAction of type \"{gameAction.GetType()}\" is unknown. It should be added to {nameof(GetGameActionDescription)}."
        };
    }
}