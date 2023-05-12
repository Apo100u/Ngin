using System;
using System.Collections.Generic;
using Ngin.Characters;

namespace Ngin.Gameplay.Turns;

public class TurnStartState : ITurnState
{
    private Turn turn;
    private Action onEnd;

    public TurnStartState(Turn turn, Action onEnd)
    {
        this.turn = turn;
        this.onEnd = onEnd;
    }

    public void Start()
    {
        List<Character> charactersInMoveOrder = new(turn.CharactersInMoveOrder);
        int amountOfCardsToDraw = Game.Settings.CardsToDrawOnTurnStart;
        
        turn.TurnCycle.Game.DrawCardsForCharacters(charactersInMoveOrder, amountOfCardsToDraw);
        onEnd?.Invoke();
    }
}