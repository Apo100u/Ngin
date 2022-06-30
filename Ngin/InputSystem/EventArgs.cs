using System;
using System.Collections.Generic;
using Ngin.Cards;

namespace Ngin.InputSystem;

public class CardsChosenEventArgs : EventArgs
{
    public readonly List<Card> chosenCards;

    public CardsChosenEventArgs(List<Card> chosenCards)
    {
        this.chosenCards = chosenCards;
    }
}