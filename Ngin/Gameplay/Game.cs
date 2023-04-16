using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ngin.Cards.Effects;
using Ngin.Cards.Targeting;
using Ngin.Characters;
using Ngin.Gameplay.Turns;
using Ngin.InputSystem;
using Ngin.LogSystem;

namespace Ngin.Gameplay;

public class Game
{
    public static GameSettings Settings { get; private set; }
    
    public readonly Input Input;
    public readonly ILog Log;
    
    public bool IsFinished { get; private set; }
    public ReadOnlyCollection<Character> AllCharacters => allCharacters.AsReadOnly();
    
    private Turn currentTurn;
    private Team[] teams;
    private List<Character> allCharacters = new();
    private int turnNumber;

    public Game(ILog log, GameSettings settings)
    {
        Input = new Input();
        Log = log;
        Settings = settings;
    }

    public void SetTeams(params Team[] teams)
    {
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
        turnNumber++;
        Log.TurnStart(turnNumber);
        
        currentTurn = new Turn(this, OnTurnEnded);
        currentTurn.Start();
    }

    private void OnTurnEnded()
    {
        Log.TurnEnd(turnNumber);
        
        if (!IsFinished)
        {
            StartNewTurn();
        }
    }
    
    public void DrawCardsForAllCharacters(int cardsCount)
    {
        Draw draw = new(cardsCount, CharacterTargetingType.User);
        
        for (int i = 0; i < allCharacters.Count; i++)
        {
            allCharacters[i].ApplyDraw(draw);
        }
    }
}