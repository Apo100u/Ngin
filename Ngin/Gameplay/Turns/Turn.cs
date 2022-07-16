using System;
using System.Collections.Generic;
using Ngin.Characters;

namespace Ngin.Gameplay.Turns;

public class Turn
{
    public event Action Ended;

    public readonly Game Game;
    
    private ITurnState turnStartState;
    private ITurnState characterMoveState;
    private Queue<Character> charactersInMoveOrder;

    public Turn(Game game)
    {
        Game = game;
    }

    public void Start()
    {
        charactersInMoveOrder = GetCharactersInMoveOrder();
        
        turnStartState = new TurnStartState(this);
        turnStartState.Ended += OnTurnStartStateEnded;
        turnStartState.Start();
    }

    private void OnTurnStartStateEnded()
    {
        turnStartState.Ended -= OnTurnStartStateEnded;
        DoNextCharactersMove();
    }

    private void DoNextCharactersMove()
    {
        Character characterOnMove = charactersInMoveOrder.Dequeue();
        characterMoveState = new CharacterMoveState(characterOnMove);
        characterMoveState.Ended += OnCharactersMoveEnded;
        characterMoveState.Start();
    }

    private void OnCharactersMoveEnded()
    {
        characterMoveState.Ended -= OnCharactersMoveEnded;

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
        allCharacters.Sort(CharactersHelper.InitiativeComparisonWithRandomOnEqual());

        for (int i = allCharacters.Count - 1; i >= 0; i--)
        {
            charactersInOrder.Enqueue(allCharacters[i]);
        }

        return charactersInOrder;
    }

    private void End()
    {
        Ended?.Invoke();
    }
}