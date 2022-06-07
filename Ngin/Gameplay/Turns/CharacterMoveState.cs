using System;
using Ngin.Characters;

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
        Game.InputSystem.ChooseCardFromHand(character, OnCardChoosen);
    }
}