using System;
using Ngin.Characters;

namespace Ngin.Gameplay.Turns;

public class TurnCycle
{
    public event Action<Turn> TurnStarting;
    public event Action<Turn> TurnEnded;
    public event Action<Character> CharacterMoveStarted;

    public readonly Game Game;

    private Turn currentTurn;
    private int turnNumber;

    public TurnCycle(Game game)
    {
        Game = game;
    }

    public void Start()
    {
        Game.DrawCardsForAllAliveCharacters(Game.Settings.CardsToDrawOnGameStart);
        StartNewTurn();
    }
    
    private void StartNewTurn()
    {
        turnNumber++;
        currentTurn = new Turn(this, turnNumber, OnCharacterMoveStarted,  OnTurnEnded);
        
        TurnStarting?.Invoke(currentTurn);
        
        currentTurn.Start();
    }

    private void OnCharacterMoveStarted(Character character)
    {
        CharacterMoveStarted?.Invoke(character);
    }

    private void OnTurnEnded()
    {
        TurnEnded?.Invoke(currentTurn);
        
        if (!Game.IsFinished)
        {
            StartNewTurn();
        }
    }
}