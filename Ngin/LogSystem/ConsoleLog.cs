using System;
using Ngin.Characters;

namespace Ngin.LogSystem;

public class ConsoleLog : ILog
{
    private void Separator()
    {
        Console.WriteLine("──────────────────────────────────────────────────────────────");
    }

    public void TurnStart(int turnNumber)
    {
        Separator();
        Console.WriteLine($"Turn {turnNumber} started.");
    }

    public void TurnEnd(int turnNumber)
    {
        Separator();
        Console.WriteLine($"Turn {turnNumber} ended.");
    }

    public void CharacterMoveStart(Character characterOnMove)
    {
        Separator();
        Console.WriteLine($"{characterOnMove.Name}'s move.");
    }
}