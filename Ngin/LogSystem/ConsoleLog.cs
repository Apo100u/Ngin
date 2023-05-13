using System;
using System.Linq;
using Ngin.Characters;
using Ngin.GameParticipants;
using Ngin.Gameplay;
using Ngin.Gameplay.Turns;

namespace Ngin.LogSystem;

public class ConsoleLog : Log
{
    private const string CharacterActionMarker = " => ";
    
    private void Separator()
    {
        Console.WriteLine("──────────────────────────────────────────────────────────────");
    }
    
    protected override void OnCharacterPassedTurn(Character character)
    {
        Console.WriteLine($"{CharacterActionMarker}{character.Name} passes.");
    }

    protected override void OnCharacterPlayedCardFromHand(PlayedCardFromHandEventArgs args)
    {
        Console.WriteLine($"{CharacterActionMarker}{args.Character.Name} plays card: {args.PlayedCard.Name}.");
    }

    protected override void OnCharacterDrawnCard(Character character)
    {
        Console.WriteLine($"{CharacterActionMarker}{character.Name} draws a card.");
    }

    protected override void OnCharacterDamaged(DamagedEventArgs args)
    {
        Console.WriteLine($"{CharacterActionMarker}{args.Character.Name} is damaged for {args.ActualDamageTaken}.");
    }

    protected override void OnCharacterHealed(HealedEventArgs args)
    {
        Console.WriteLine($"{CharacterActionMarker}{args.Character.Name} is healed for {args.ActualHealTaken}.");
    }

    protected override void GameFinished(Game game)
    {
        Separator();
        GameParticipant winningGameParticipant = game.Participants.First(x => !x.IsEveryCharacterDead());
        Console.WriteLine($"Game ended! Winner: {winningGameParticipant.Name}.");
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
        if (character.Owner is HumanPlayer)
        {
            Separator();
            ShowGameStatus(character.Game);
        }

        Separator();
        Console.WriteLine($"{character.Name}'s move.");
    }

    protected override void OnCharacterTryingToDrawFromEmptyDeck(Character character)
    {
        Separator();
        Console.WriteLine($"Character {character.Name} is trying to draw a card from an empty deck. This will apply damage equal to their current health.");
    }

    protected override void OnCharacterDied(Character character)
    {
        Separator();
        Console.WriteLine($"Character {character.Name} dies.");
    }

    private void ShowGameStatus(Game game)
    {
        for (int i = 0; i < game.Participants.Length; i++)
        {
            ShowParticipantStatus(game.Participants[i]);
        }
    }

    private void ShowParticipantStatus(GameParticipant gameParticipant)
    {
        Console.WriteLine(gameParticipant.Name);
        
        for (int i = 0; i < gameParticipant.OwnedCharacters.Length; i++)
        {
            Character character = gameParticipant.OwnedCharacters[i];
            
            Console.WriteLine($"   -{character.Name}   | Health: {character.Health.Current} | Initiative: {character.Initiative.Current} " +
                              $"| Cards in hand: {character.Hand.Count} | Cards in deck: {character.Deck.Count} |");
        }
    }
}