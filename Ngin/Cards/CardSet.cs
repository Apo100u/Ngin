using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ngin.Cards;

public class CardSet
{
    public ReadOnlyCollection<Card> Cards => cards.AsReadOnly();
    
    private List<Card> cards = new();
}