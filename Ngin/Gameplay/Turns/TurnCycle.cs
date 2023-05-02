namespace Ngin.Gameplay.Turns;

public class TurnCycle
{
    public readonly Game Game;
    
    private Turn currentTurn;
    private int turnNumber;

    public TurnCycle(Game game)
    {
        Game = game;
    }

    public void Start()
    {
        Game.DrawCardsForAllCharacters(Game.Settings.CardsToDrawOnGameStart);
        StartNewTurn();
    }
    
    private void StartNewTurn()
    {
        turnNumber++;
        Game.Log.TurnStart(turnNumber);
        
        currentTurn = new Turn(this, OnTurnEnded);
        currentTurn.Start();
    }
    
    private void OnTurnEnded()
    {
        Game.Log.TurnEnd(turnNumber);
        
        if (!Game.IsFinished)
        {
            StartNewTurn();
        }
    }
}