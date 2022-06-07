using System;

namespace Ngin.Gameplay.Turns;

public class TurnStartState : ITurnState
{
    public event Action Ended;

    private Turn turn;

    public TurnStartState(Turn turn)
    {
        this.turn = turn;
    }

    public void Start()
    {
        int amountOfCardsToDraw = turn.Game.Settings.CardsToDrawOnTurnStart;
        turn.Game.DrawCardsForAllCharacters(amountOfCardsToDraw);
        Ended?.Invoke();
    }
}