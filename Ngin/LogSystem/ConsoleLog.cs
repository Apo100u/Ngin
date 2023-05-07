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

    protected override void OnCharacterTryingToDrawFromEmptyDeck(Character character)
    {
        Separator();
        Console.WriteLine($"Character {character.Name} is trying to draw a card from empty deck. This will apply damage equal to their current health.");
    }
}