using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.Gameplay.Turns;

namespace Ngin.LogSystem;

public abstract class Log
{
    public void StartLogging(Game game)
    {
        game.Finished += GameFinished;
        game.TurnCycle.TurnStarting += OnTurnStarting;
        game.TurnCycle.TurnEnded += OnTurnEnded;
        game.TurnCycle.CharacterMoveStarted += OnCharacterMoveStarted;

        for (int i = 0; i < game.AllCharacters.Count; i++)
        {
            game.AllCharacters[i].TryingToDrawFromEmptyDeck += OnCharacterTryingToDrawFromEmptyDeck;
            game.AllCharacters[i].Died += OnCharacterDied;
        }
    }

    protected abstract void GameFinished(Game game);
    protected abstract void OnTurnStarting(Turn turn);
    protected abstract void OnTurnEnded(Turn turn);
    protected abstract void OnCharacterMoveStarted(Character character);
    protected abstract void OnCharacterTryingToDrawFromEmptyDeck(Character character);
    protected abstract void OnCharacterDied(Character character);
}