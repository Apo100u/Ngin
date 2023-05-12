using System;
using System.Collections.Generic;
using Ngin.Characters;

namespace Ngin.Gameplay.Turns;

public class Turn
{
    public readonly TurnCycle TurnCycle;
    public readonly int Number;
    
    public Queue<Character> CharactersInMoveOrder { get; private set; }
    
    private ITurnState currentState;
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
        CharactersInMoveOrder = GetAliveCharactersInMoveOrder(TurnCycle.Game.AllCharacters);

        for (int i = 0; i < TurnCycle.Game.AllCharacters.Count; i++)
        {
            TurnCycle.Game.AllCharacters[i].Died += OnCharacterDied;
        }
        
        currentState = new TurnStartState(this, OnTurnStartStateEnded);
        currentState.Start();
    }

    private void OnCharacterDied(Character character)
    {
        UpdateMoveOrderOfCharactersLeft();
    }

    private void OnTurnStartStateEnded()
    {
        DoNextCharactersMove();
    }

    private void DoNextCharactersMove()
    {
        Character characterOnMove = CharactersInMoveOrder.Dequeue();
        
        onCharacterMoveStarted?.Invoke(characterOnMove);
        
        currentState = new CharacterMoveState(characterOnMove, OnCharactersMoveEnded);
        currentState.Start();
    }

    private void OnCharactersMoveEnded()
    {
        if (CharactersInMoveOrder.Count > 0)
        {
            DoNextCharactersMove();
        }
        else
        {
            End();
        }
    }

    private void UpdateMoveOrderOfCharactersLeft()
    {
        CharactersInMoveOrder = GetAliveCharactersInMoveOrder(CharactersInMoveOrder);
    }

    private Queue<Character> GetAliveCharactersInMoveOrder(IEnumerable<Character> source)
    {
        Queue<Character> aliveCharactersInMoveOrder = new();
        List<Character> allCharacters = new(source);
        allCharacters.Sort(RNG.InitiativeComparisonWithRandomOnEqual());

        for (int i = allCharacters.Count - 1; i >= 0; i--)
        {
            if (!allCharacters[i].IsDead)
            {
                aliveCharactersInMoveOrder.Enqueue(allCharacters[i]);
            }
        }

        return aliveCharactersInMoveOrder;
    }

    private void End()
    {
        for (int i = 0; i < TurnCycle.Game.AllCharacters.Count; i++)
        {
            TurnCycle.Game.AllCharacters[i].Died -= OnCharacterDied;
        }
        
        onEnd?.Invoke();
    }
}