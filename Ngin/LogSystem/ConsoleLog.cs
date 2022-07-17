using System;
using System.Collections.Generic;
using Ngin.Cards;

namespace Ngin.LogSystem;

public class ConsoleLog : ILog
{
    private void LogSeparator()
    {
        Console.WriteLine("------------------------------------------------------------------------");
    }
    
    public void LogInvalidInput(string invalidInput, List<string> validInputs)
    {
        LogSeparator();
        
        string message = $"Sorry, {invalidInput} is invalid input.";

        if (validInputs?.Count > 0)
        {
            message = string.Concat(message, $" Valid inputs are: {string.Join(", ", validInputs)}.");
        }
        
        Console.WriteLine(message);
    }

    public void LogCardsToChooseFrom(Dictionary<string, Card> cardsByInput)
    {
        LogSeparator();
        Console.WriteLine("Choose card:");

        foreach (KeyValuePair<string, Card> cardByInput in cardsByInput)
        {
            Console.WriteLine($"Type {cardByInput.Key} to choose {cardByInput.Value.Name}.");
        }
    }

    public void LogCardChoice(Card chosenCard)
    {
        LogSeparator();
        Console.WriteLine($"Chosen card: {chosenCard.Name}");
    }

    public void LogPassInput(string passInput)
    {
        Console.WriteLine($"Type {passInput} to pass.");
    }
}