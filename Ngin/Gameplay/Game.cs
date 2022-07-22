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
    
    public bool IsFinished { get; private set; }
    public ReadOnlyCollection<Character> AllCharacters => allCharacters.AsReadOnly();
    
    private Turn currentTurn;
    private Team[] teams;
    private List<Character> allCharacters = new();

    public Game(ILog log, GameSettings settings, params Team[] teams)
    {
        Input = new Input();
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
        currentTurn = new Turn(this, OnTurnEnded);
        currentTurn.Start();
    }

    private void OnTurnEnded()
    {
        if (!IsFinished)
        {
            StartNewTurn();
        }
    }
    
    public void DrawCardsForAllCharacters(int cardsCount)
    {
        for (int i = 0; i < allCharacters.Count; i++)
        {
            allCharacters[i].Draw(cardsCount);
        }
    }
}