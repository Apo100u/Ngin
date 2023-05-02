using System;
using System.Collections.Generic;
using Ngin.Characters;

namespace Ngin.Gameplay.Turns;

public class Turn
{
    public readonly TurnCycle TurnCycle;
    public readonly int Number;
    
    private ITurnState currentState;
    private Queue<Character> charactersInMoveOrder;
    private Action<Character> onCharacterMoveStarted;
    private Action onEnd;

    public Turn(TurnCycle turnCycle, int number, Action<Character> onCharacterMoveStarted, Action onEnd)
    {
        TurnCycle = turnCycle;
        Number = number;
        this.onCharacterMoveStarted = onCharacterMoveStarted;
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
        
        onCharacterMoveStarted?.Invoke(characterOnMove);
        
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
        List<Character> allCharacters = new(TurnCycle.Game.AllCharacters);
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