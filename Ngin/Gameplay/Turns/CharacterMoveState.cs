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
        UpdateInputActions();
    }

    private void UpdateInputActions()
    {
        Game.Input.ClearAllowedActions();
        Game.Input.AllowPass(OnPass);
        Game.Input.AllowChoosingCardFromSet(character.Hand, OnCardToPlayChosen);
    }

    private void OnPass()
    {
        End();
    }

    private void OnCardToPlayChosen(Card card)
    {
        PlayChosenCard(card);

        bool canPlayNextCard = playedCardsCount < Game.Settings.CardsAllowedToPlayPerTurn;

        if (canPlayNextCard)
        {
            UpdateInputActions();
        }
        else
        {
            End();
        }
    }
    
    private void PlayChosenCard(Card chosenCard)
    {
        chosenCard.Play();
        playedCardsCount++;
    }

    private void End()
    {
        onEnd?.Invoke();
    }
}