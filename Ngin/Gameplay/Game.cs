using System;
using System.Collections.Generic;
using Ngin.Cards.Effects;
using Ngin.Cards.Targeting;
using Ngin.Characters;
using Ngin.GameParticipants;
using Ngin.Gameplay.Turns;
using Ngin.InputSystem;

namespace Ngin.Gameplay;

public class Game
{
    public event Action<Game> Finished;
    
    public static GameSettings Settings { get; private set; }
    
    public readonly TurnCycle TurnCycle;
    
    public bool IsFinished { get; private set; }
    public Input Input { get; private set; }
    public GameParticipant[] Participants { get; private set; }
    public List<Character> AllCharacters { get; private set; }

    public Game(GameSettings settings)
    {
        TurnCycle = new TurnCycle(this);
        Settings = settings;
    }

    public void SetInput(Input input)
    {
        Input = input;
    }

    public void SetParticipants(params GameParticipant[] participants)
    {
        Participants = participants;
        AllCharacters = new List<Character>();

        for (int i = 0; i < participants.Length; i++)
        {
            AllCharacters.AddRange(participants[i].OwnedCharacters);
        }
    }

    public void Start()
    {
        for (int i = 0; i < AllCharacters.Count; i++)
        {
            AllCharacters[i].Died += OnCharacterDied;
            AllCharacters[i].ShuffleDeck();
        }
        
        DrawCardsForCharacters(AllCharacters, Settings.CardsToDrawOnGameStart);
        TurnCycle.Start();
    }

    public void DrawCardsForCharacters(List<Character> characters, int cardsCount)
    {
        Draw draw = new(cardsCount, CharacterTargetingType.User);
        
        for (int i = 0; i < characters.Count; i++)
        {
            if (!characters[i].IsDead)
            {
                characters[i].ApplyDraw(draw);
            }
        }
    }

    private void OnCharacterDied(Character character)
    {
        CheckForGameFinish();
    }

    private void CheckForGameFinish()
    {
        int countOfRemainingParticipants = 0;

        for (int i = 0; i < Participants.Length; i++)
        {
            if (!Participants[i].IsEveryCharacterDead())
            {
                countOfRemainingParticipants++;
            }
        }
        
        if (countOfRemainingParticipants == 1)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        IsFinished = true;
        Finished?.Invoke(this);
    }
}