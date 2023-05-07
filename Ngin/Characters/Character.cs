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
    public event Action<Character> TryingToDrawFromEmptyDeck;
    public event Action<Character> Died;

    public readonly Game Game;
    public readonly string Name;
    public readonly Statistic Health;
    public readonly Statistic Initiative;
    public readonly List<Card> Hand = new();
    public readonly Stack<Card> Deck = new();

    public bool IsDead { get; private set; }
    public Team Team { get; private set; }
    
    public Character(Game game, string name, int baseHealth, int baseInitiative)
    {
        Game = game;
        Name = name;
        Health = new Statistic(baseHealth);
        Initiative = new Statistic(baseInitiative);
        IsDead = false;
    }

    public void SetTeam(Team team)
    {
        Team = team;
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
        }
    }
}