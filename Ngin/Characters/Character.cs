using System;
using System.Collections.Generic;
using Ngin.Cards;
using Ngin.Cards.Effects;
using Ngin.Cards.Targeting;
using Ngin.Gameplay;
using Ngin.Helpers.Calculators;

namespace Ngin.Characters;

public class Character
{
    public event Action<Character> DrawnCard;
    public event Action<DamagedEventArgs> Damaged;
    public event Action<HealedEventArgs> Healed;
    public event Action<Character> TryingToDrawFromEmptyDeck;
    public event Action<Character> Died;

    public readonly Game Game;
    public readonly string Name;
    public readonly Statistic Health;
    public readonly Statistic Initiative;

    public List<Card> Hand { get; private set; }
    public Stack<Card> Deck { get; private set; }
    public bool IsDead { get; private set; }
    public Team Team { get; private set; }
    
    public Character(Game game, string name, int baseHealth, int baseInitiative, IEnumerable<Card> deckCards)
    {
        Game = game;
        Name = name;
        Health = new Statistic(baseHealth);
        Initiative = new Statistic(baseInitiative);
        Hand = new List<Card>();
        Deck = new Stack<Card>(deckCards);
        IsDead = false;
    }

    public void SetTeam(Team team)
    {
        Team = team;
    }

    public void ShuffleDeck()
    {
        Deck = RNG.ShuffleCollection(Deck);
    }
    
    public void ApplyDraw(Draw draw)
    {
        int cardsDrawnCount = new DrawCalculator(this, draw).CalculateCardsDrawnCount();

        for (int i = 0; i < cardsDrawnCount; i++)
        {
            if (!IsDead)
            {
                if (Deck.Count > 0)
                {
                    Card drawnCard = Deck.Pop();
                    Hand.Add(drawnCard);
                    DrawnCard?.Invoke(this);
                }
                else
                {
                    TryingToDrawFromEmptyDeck?.Invoke(this);
                    Damage damageForDrawWithEmptyDeck = new(Health.Current, CharacterTargetingType.User);
                    ApplyDamage(damageForDrawWithEmptyDeck);
                }
            }
        }
    }

    public void ApplyDamage(Damage damage)
    {
        if (!IsDead)
        {
            int damagePower = new DamageCalculator(this, damage).CalculateDamagePower();
            Health.ChangeBy(-damagePower);
            
            Damaged?.Invoke(new DamagedEventArgs(this, damagePower));

            if (Health.Current <= 0 && !IsDead)
            {
                IsDead = true;
                Died?.Invoke(this);
            }
        }
    }

    public void ApplyHeal(Heal heal)
    {
        if (!IsDead)
        {
            int healPower = new HealCalculator(this, heal).CalculateHealPower();
            Health.ChangeBy(healPower);
            
            Healed?.Invoke(new HealedEventArgs(this, healPower));
        }
    }
}