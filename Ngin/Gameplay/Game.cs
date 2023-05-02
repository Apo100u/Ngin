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

    private TurnCycle turnCycle;
    private Team[] teams;
    private List<Character> allCharacters = new();

    public Game(ILog log, GameSettings settings)
    {
        Input = new Input();
        turnCycle = new TurnCycle(this);
        Log = log;
        Settings = settings;
    }

    public void SetTeams(params Team[] teams)
    {
        this.teams = teams;
        allCharacters.Clear();

        for (int i = 0; i < teams.Length; i++)
        {
            allCharacters.AddRange(teams[i].Characters);
        }
    }

    public void Start()
    {
        turnCycle.Start();
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