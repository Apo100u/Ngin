using System;
using Ngin.Characters;
using Ngin.GameParticipants;
using Ngin.GameParticipants.AI;
using Ngin.Gameplay;
using Ngin.Helpers.Cards;
using Ngin.InputSystem;
using Ngin.LogSystem;

namespace Ngin;

internal class Program
{
    public static void Main(string[] args)
    {
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

        GameParticipant team1 = new HumanPlayer(game, "Participant 1", character1, character2);
        GameParticipant team2 = new RandomAi(game, "Participant 2", character3, character4);
        
        game.SetParticipants(team1, team2);
        
        ConsoleLog consoleLog = new();
        consoleLog.StartLogging(game);

        game.SetInput(new ConsoleInput());
        
        game.Start();
        
        while (!game.IsFinished)
        {
            game.Input.ParticipantOnMove.MakeMove();
        }

        Console.ReadKey();
    }
}