using System;
using Ngin.Cards;

namespace Ngin.InputSystem;

public class ConsoleInputSystem : IInputSystem
{
    public CardChooser ChooseCardsFromSet(CardSet cardSet, Action<CardsChosenEventArgs> onCardChosen)
    {
        CardChooser cardChooser =  new ConsoleCardChooser(cardSet, onCardChosen);
        return cardChooser;
    }
}