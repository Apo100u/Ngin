using System.Collections.Generic;

namespace Ngin.Cards;

public class CardSet
{
    public CardSet()
    {
        Card testCard1 = new Card("Jebnięcie");
        Card testCard2 = new Card("Pizgnięcie");
        Card testCard3 = new Card("Pierdolnięcie");
        Card testCard4 = new Card("Zajebanie");
        
        cards.Add(testCard1);
        cards.Add(testCard2);
        cards.Add(testCard3);
        cards.Add(testCard4);
    }

    public int Count => cards.Count;
    
    private List<Card> cards = new();
    
    public Card this[int index] => cards[index];
}