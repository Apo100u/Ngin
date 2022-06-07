using System;
using Ngin.Cards;

namespace Ngin.InputSystem;

public class CardChosenEventArgs : EventArgs
{
    public readonly Card card;

    public CardChosenEventArgs(Card card)
    {
        this.card = card;
    }
}