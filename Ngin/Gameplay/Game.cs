using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public ReadOnlyCollection<Character> AllCharacters => allCharacters.AsReadOnly();

    private List<Character> allCharacters = new();

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
        allCharacters.Clear();

        for (int i = 0; i < participants.Length; i++)
        {
            allCharacters.AddRange(participants[i].OwnedCharacters);
        }
    }

    public void Start()
    {
        for (int i = 0; i < allCharacters.Count; i++)
        {
            allCharacters[i].Died += OnCharacterDied;
            allCharacters[i].ShuffleDeck();
        }
        
        DrawCardsForAllAliveCharacters(Settings.CardsToDrawOnGameStart);
        TurnCycle.Start();
    }

    public void DrawCardsForAllAliveCharacters(int cardsCount)
    {
        Draw draw = new(cardsCount, CharacterTargetingType.User);
        
        for (int i = 0; i < allCharacters.Count; i++)
        {
            if (!allCharacters[i].IsDead)
            {
                allCharacters[i].ApplyDraw(draw);
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

        // TODO (BUG): Game is not finished properly. After finishing once, the remaining actions are still executed and the game can "finish" again.
    }
}