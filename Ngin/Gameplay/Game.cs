using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ngin.Characters;
using Ngin.Gameplay.Turns;
using Ngin.InputSystem;
using Ngin.LogSystem;

namespace Ngin.Gameplay;

public class Game
{
    public static Input Input { get; private set; }
    public static ILog Log { get; private set; }
    public static GameSettings Settings { get; private set; }
    
    public ReadOnlyCollection<Character> AllCharacters => allCharacters.AsReadOnly();
    
    private Turn currentTurn;
    private Team[] teams;
    private List<Character> allCharacters = new();

    public Game(Input input, ILog log, GameSettings settings, params Team[] teams)
    {
        Input = input;
        Log = log;
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
        currentTurn.Start();
    }
    
    public void DrawCardsForAllCharacters(int cardsCount)
    {
        for (int i = 0; i < allCharacters.Count; i++)
        {
            allCharacters[i].Draw(cardsCount);
        }
    }
}