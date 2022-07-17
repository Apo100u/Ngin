using System;
using Ngin.Cards;
using Ngin.Characters;
using Ngin.Errors;
using Ngin.InputSystem;

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
        ChooseCardToPlay();
    }

    private void ChooseCardToPlay()
    {
        Game.Input.ChooseCardsFromSet(character.Hand, OnCardToPlayChosen)
            .SetMaxAmount(1)
            .AllowPassing()
            .Start();
    }

    private void OnCardToPlayChosen(CardsChosenEventArgs args)
    {
        CheckForErrorsInChosenCards(args);

        if (!args.ChoosingPassed)
        {
            Card chosenCard = args.ChosenCards[0];
            PlayChosenCard(chosenCard);
        }

        bool canPlayNextCard = playedCardsCount < Game.Settings.CardsAllowedToPlayPerTurn && !args.ChoosingPassed;

        if (canPlayNextCard)
        {
            ChooseCardToPlay();
        }
        else
        {
            End();
        }
    }

    private void CheckForErrorsInChosenCards(CardsChosenEventArgs args)
    {
        if (args.ChosenCards.Count > 1)
        {
            throw new UnexpectedAmountException($"Amount of chosen cards to play during characters turn ({character.Name}) was more than 1. " +
                                                "This should be impossible.");
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