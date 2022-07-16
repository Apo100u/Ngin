using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ngin.Characters;
using Ngin.Gameplay.Turns;
using Ngin.InputSystem;
using Ngin.LogSystem;

namespace Ngin.Gameplay;

public class Game
{
    public static IInputSystem InputSystem { get; private set; }
    public static ILogSystem LogSystem { get; private set; }
    public static GameSettings Settings { get; private set; }
    
    public ReadOnlyCollection<Character> AllCharacters => allCharacters.AsReadOnly();
    
    private Turn currentTurn;
    private Team[] teams;
    private List<Character> allCharacters = new();

    public Game(IInputSystem inputSystem, ILogSystem logSystem, GameSettings settings, params Team[] teams)
    {
        InputSystem = inputSystem;
        LogSystem = logSystem;
        Settings = settings;
        
        this.teams = teams;

        for (int i = 0; i < teams.Length; i++)
        {
            allCharacters.AddRange(teams[i].Characters);
        }
    }

    public void Start()
    {
        DrawCardsForAllCharacters(Settings.CardsToDrawOnGameStart);
        StartNewTurn();
    }

    private void StartNewTurn()
    {
        // TODO: Crucial refactor needed!
        // Current approach with events creates huge call stack and potential stack overflow, also the calling method never exits, so non-used objects might
        // never get garbage collected. Consider using loops or multi-threading. Also keep in mind that things related to user input must be asynchronous, so
        // it works in other environments than console, for example Unity.
        
        currentTurn = new Turn(this);
        currentTurn.Ended += OnTurnEnded;
        currentTurn.Start();
    }

    private void OnTurnEnded()
    {
        currentTurn.Ended -= OnTurnEnded;
        StartNewTurn();
    }
    
    public void DrawCardsForAllCharacters(int cardsCount)
    {
        for (int i = 0; i < allCharacters.Count; i++)
        {
            allCharacters[i].Draw(cardsCount);
        }
    }
}