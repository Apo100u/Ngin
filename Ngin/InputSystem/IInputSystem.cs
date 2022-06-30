using System;
using Ngin.Cards;

namespace Ngin.InputSystem;

public interface IInputSystem
{
    public CardChooser ChooseCardsFromSet(CardSet cardSet, Action<CardsChosenEventArgs> onCardChosen);
}