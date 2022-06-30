using System;
using Ngin.Characters;
using Ngin.InputSystem;

namespace Ngin.Gameplay.Turns;

public class CharacterMoveState : ITurnState
{
    public event Action Ended;

    private Character character;

    public CharacterMoveState(Character character)
    {
        this.character = character;
    }

    public void Start()
    {
        ChooseCardToPlay();
    }

    private void ChooseCardToPlay()
    {
        Game.InputSystem.ChooseCardsFromSet(character.Hand, OnCardsChosen)
            .SetMaxAmount(1)
            .AllowPassing()
            .Start();
    }

    private void OnCardsChosen(CardsChosenEventArgs args)
    {
    }
}