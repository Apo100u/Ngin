using System;
using System.Collections.Generic;
using Ngin.Cards;

namespace Ngin.InputSystem;

public class CardsChosenEventArgs : EventArgs
{
    public readonly List<Card> ChosenCards;
    public readonly bool ChoosingPassed;

    public CardsChosenEventArgs(List<Card> chosenCards, bool choosingPassed)
    {
        ChosenCards = chosenCards;
        ChoosingPassed = choosingPassed;
    }
}