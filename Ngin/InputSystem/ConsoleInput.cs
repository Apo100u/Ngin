using System;
using System.Linq;
using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.InputSystem.Actions;

namespace Ngin.InputSystem;

public class ConsoleInput : Input
{
    public ConsoleInput(Game game) : base(game)
    {
    }
    
    public override GameAction ReadUserActionChoice()
    {
        LogAllowedActions();

        int chosenActionIndex;
        
        while (!TryGetValidActionInput(out chosenActionIndex))
        {
            Console.WriteLine("Invalid input, try again.");
        }

        return AllowedActions[chosenActionIndex];
    }
    
    private bool TryGetValidActionInput(out int chosenActionIndex)
    {
        string userInput = Console.ReadLine();
        bool isInputValid = int.TryParse(userInput, out chosenActionIndex) && chosenActionIndex >= 0 && chosenActionIndex < AllowedActions.Count;
        return isInputValid;
    }
    
    private void LogAllowedActions()
    {
        int gameActionNumber = 0;
        
        foreach (GameAction allowedAction in AllowedActions)
        {
            string actionConsoleDescription = GetGameActionDescription(allowedAction);
            Console.WriteLine($"{gameActionNumber} - {actionConsoleDescription}");
            
            gameActionNumber++;
        }
    }
    
    private string GetGameActionDescription(GameAction gameAction)
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