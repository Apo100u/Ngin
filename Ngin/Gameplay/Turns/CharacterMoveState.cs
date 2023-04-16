using System;
using Ngin.Cards;
using Ngin.Characters;

namespace Ngin.Gameplay.Turns;

public class CharacterMoveState : ITurnState
{
    private int playedCardsCount;
    private Character character;
    private Action onEnd;

    public CharacterMoveState(Character character, Action onEnd)
    {
        this.character = character;
        this.onEnd = onEnd;
    }

    public void Start()
    {
        SetupInput();
    }

    private void SetupInput()
    {
        character.Game.Input.ClearAllowedActions();
        character.Game.Input.AllowPassing(OnPass);
        character.Game.Input.AllowChoosingCardFromCollection(character.Hand, OnCardToPlayChosen);
    }

    private void OnPass()
    {
        End();
    }

    private void OnCardToPlayChosen(Card card)
    {
        PlayChosenCard(card);
    }
    
    private void PlayChosenCard(Card chosenCard)
    {
        chosenCard.Play(character, OnChosenCardPlayed, OnChosenCardCancelled);
    }

    private void OnChosenCardPlayed()
    {
        playedCardsCount++;

        bool canPlayNextCard = playedCardsCount < Game.Settings.CardsAllowedToPlayPerTurn;

        if (canPlayNextCard)
        {
            SetupInput();
        }
        else
        {
            End();
        }
    }

    private void OnChosenCardCancelled()
    {
        SetupInput();
    }

    private void End()
    {
        onEnd?.Invoke();
    }
}