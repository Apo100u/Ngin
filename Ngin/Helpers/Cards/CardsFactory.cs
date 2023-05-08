using System.Collections.Generic;
using Ngin.Cards;
using Ngin.Cards.Effects;
using Ngin.Cards.Targeting;

namespace Ngin.Helpers.Cards;

public static class CardsFactory
{
    public static IEnumerable<Card> SimpleExampleDeck()
    {
        Card[] simpleExampleDeck = new Card[30];

        for (int i = 0; i < 20; i++)
        {
            simpleExampleDeck[i] = DamageEnemyForThreeCard();
        }
        
        for (int i = 20; i < 27; i++)
        {
            simpleExampleDeck[i] = HealAllyOrUserForOneCard();
        }
        
        for (int i = 27; i < 30; i++)
        {
            simpleExampleDeck[i] = DrawOneForUserCard();
        }

        return simpleExampleDeck;
    }

    public static Card DamageEnemyForThreeCard()
    {
        return new Card("Example Damage Card", new Damage(3, CharacterTargetingType.AliveEnemy));
    }

    public static Card HealAllyOrUserForOneCard()
    {
        return new Card("Example Heal Card", new Heal(1, CharacterTargetingType.AliveAllyOrUser));
    }

    public static Card DrawOneForUserCard()
    {
        return new Card("Example Draw Card", new Draw(1, CharacterTargetingType.User));
    }
}