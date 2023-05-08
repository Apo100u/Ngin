using System;
using System.Linq;
using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.Gameplay.Turns;

namespace Ngin.LogSystem;

public class ConsoleLog : Log
{
    private const string CardEffectMarker = " => ";
    
    private void Separator()
    {
        Console.WriteLine("──────────────────────────────────────────────────────────────");
    }

    protected override void OnCharacterDrawnCard(Character character)
    {
        Console.WriteLine($"{CardEffectMarker}{character.Name} drawn a card.");
    }

    protected override void OnCharacterDamaged(DamagedEventArgs args)
    {
        Console.WriteLine($"{CardEffectMarker}{args.Character.Name} was damaged for {args.ActualDamageTaken}.");
    }

    protected override void OnCharacterHealed(HealedEventArgs args)
    {
        Console.WriteLine($"{CardEffectMarker}{args.Character.Name} was healed for {args.ActualHealTaken}.");
    }

    protected override void GameFinished(Game game)
    {
        Separator();

        Team winningTeam = game.AllCharacters.First(x => !x.Team.AllCharactersDead()).Team;
        Console.WriteLine($"Game ended! Winner team: {string.Join(", ", winningTeam.Characters.Select(x => x.Name))}.");
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
        ShowGameStatus(character.Game);
        Separator();
        Console.WriteLine($"{character.Name}'s move.");
    }

    protected override void OnCharacterTryingToDrawFromEmptyDeck(Character character)
    {
        Separator();
        Console.WriteLine($"Character {character.Name} is trying to draw a card from empty deck. This will apply damage equal to their current health.");
    }

    protected override void OnCharacterDied(Character character)
    {
        Separator();
        Console.WriteLine($"Character {character.Name} has died.");
    }

    private void ShowGameStatus(Game game)
    {
        for (int i = 0; i < game.Teams.Length; i++)
        {
            ShowTeamStatus(game.Teams[i]);
        }
    }

    private void ShowTeamStatus(Team team)
    {
        Console.WriteLine(team.Name);
        
        for (int i = 0; i < team.Characters.Length; i++)
        {
            Character character = team.Characters[i];
            
            Console.WriteLine($"   -{character.Name}   | Health: {character.Health.Current} | Initiative: {character.Initiative.Current} " +
                              $"| Cards in hand: {character.Hand.Count} | Cards in deck: {character.Deck.Count} |");
        }
    }
}