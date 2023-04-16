using System;
using System.Collections.Generic;
using Ngin.Characters;

namespace Ngin.Gameplay.Turns;

public class Turn
{
    public readonly Game Game;
    
    private ITurnState currentState;
    private Queue<Character> charactersInMoveOrder;
    private Action onEnd;

    public Turn(Game game, Action onEnd)
    {
        Game = game;
        this.onEnd = onEnd;
    }

    public void Start()
    {
        charactersInMoveOrder = GetCharactersInMoveOrder();
        
        currentState = new TurnStartState(this, OnTurnStartStateEnded);
        currentState.Start();
    }

    private void OnTurnStartStateEnded()
    {
        DoNextCharactersMove();
    }

    private void DoNextCharactersMove()
    {
        Character characterOnMove = charactersInMoveOrder.Dequeue();
        Game.Log.CharacterMoveStart(characterOnMove);
        
        currentState = new CharacterMoveState(characterOnMove, OnCharactersMoveEnded);
        currentState.Start();
    }

    private void OnCharactersMoveEnded()
    {
        if (charactersInMoveOrder.Count > 0)
        {
            DoNextCharactersMove();
        }
        else
        {
            End();
        }
    }

    private Queue<Character> GetCharactersInMoveOrder()
    {
        Queue<Character> charactersInOrder = new();
        List<Character> allCharacters = new(Game.AllCharacters);
        allCharacters.Sort(RNG.InitiativeComparisonWithRandomOnEqual());

        for (int i = allCharacters.Count - 1; i >= 0; i--)
        {
            charactersInOrder.Enqueue(allCharacters[i]);
        }

        return charactersInOrder;
    }

    private void End()
    {
        onEnd?.Invoke();
    }
}