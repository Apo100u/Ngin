using System;
using System.Collections.Generic;

namespace Ngin.LogSystem;

public class ConsoleLogSystem : ILogSystem
{
    public void LogCardsToChooseFrom(Dictionary<string, string> cardNamesByKeyboardKeys, string passKey = null)
    {
        Console.WriteLine("Choose card:");
            
        foreach (KeyValuePair<string, string> cardNameKeyPair in cardNamesByKeyboardKeys)
        {
            Console.WriteLine($"Press {cardNameKeyPair.Key} to choose {cardNameKeyPair.Value}");
        }

        if (!string.IsNullOrEmpty(passKey))
        {
            Console.WriteLine($"Press {passKey} to pass.");
        }
    }
}