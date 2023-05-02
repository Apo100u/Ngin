using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ngin.Cards.Effects;
using Ngin.Cards.Targeting;
using Ngin.Characters;
using Ngin.Gameplay.Turns;
using Ngin.InputSystem;

namespace Ngin.Gameplay;

public class Game
{
    public static GameSettings Settings { get; private set; }
    
    public readonly Input Input;
    public readonly TurnCycle TurnCycle;
    
    public bool IsFinished { get; private set; }
    public ReadOnlyCollection<Character> AllCharacters => allCharacters.AsReadOnly();

    private Team[] teams;
    private List<Character> allCharacters = new();

    public Game(GameSettings settings)
    {
        Input = new Input();
        TurnCycle = new TurnCycle(this);
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
        TurnCycle.Start();
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