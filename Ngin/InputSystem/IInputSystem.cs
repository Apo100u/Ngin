using System;
using Ngin.Characters;

namespace Ngin.InputSystem;

public interface IInputSystem
{
    public void ChooseCardFromHand(Character character, Action<CardChosenEventArgs> onCardChosen, bool allowPassing);
}