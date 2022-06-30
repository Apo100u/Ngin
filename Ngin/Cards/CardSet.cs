using System.Collections.Generic;

namespace Ngin.Cards;

public class CardSet
{
    public int Count => cards.Count;
    
    private List<Card> cards = new();
    
    public Card this[int index] => cards[index];
}