using System;

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
        int amountOfCardsToDraw = Game.Settings.CardsToDrawOnTurnStart;
        turn.TurnCycle.Game.DrawCardsForAllAliveCharacters(amountOfCardsToDraw);
        onEnd?.Invoke();
    }
}