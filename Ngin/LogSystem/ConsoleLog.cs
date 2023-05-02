using System;
using Ngin.Characters;
using Ngin.Gameplay.Turns;

namespace Ngin.LogSystem;

public class ConsoleLog : Log
{
    private void Separator()
    {
        Console.WriteLine("──────────────────────────────────────────────────────────────");
    }

    protected override void OnTurnStarting(Turn turn)
    {
        Separator();
        Console.WriteLine($"Turn {turn.Number} started.");
    }

    protected override void OnTurnEnded(Turn turn)
    {
        Separator();
        Console.WriteLine($"Turn {turn.Number} ended.");
    }

    protected override void OnCharacterMoveStarted(Character character)
    {
        Separator();
        Console.WriteLine($"{character.Name}'s move.");
    }
}