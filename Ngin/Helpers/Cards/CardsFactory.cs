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
            simpleExampleDeck[i] = DamageEnemyCard(3);
        }
        
        for (int i = 20; i < 27; i++)
        {
            simpleExampleDeck[i] = HealAllyOrUserCard(5);
        }
        
        for (int i = 27; i < 30; i++)
        {
            simpleExampleDeck[i] = DrawForUserCard(1);
        }

        return simpleExampleDeck;
    }

    public static Card DamageEnemyCard(int damagePower)
    {
        return new Card("Example Damage Card", new Damage(damagePower, CharacterTargetingType.AliveEnemy));
    }

    public static Card HealAllyOrUserCard(int healPower)
    {
        return new Card("Example Heal Card", new Heal(healPower, CharacterTargetingType.AliveAllyOrUser));
    }

    public static Card DrawForUserCard(int drawAmount)
    {
        return new Card("Example Draw Card", new Draw(drawAmount, CharacterTargetingType.User));
    }
}