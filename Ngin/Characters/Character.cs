using Ngin.Cards;
using Ngin.Gameplay;

namespace Ngin.Characters;

public class Character
{
    public readonly Game Game;
    public readonly string Name;
    public readonly Statistic Health;
    public readonly Statistic Initiative;
    public readonly CardSet Hand = new();
    public readonly CardSet Deck = new();

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
    
    public void Draw(int amount)
    {
    }

    public void DealDamage(int damage)
    {
        // There is a possibility that this method will grow too much, because there might be lots of things that alter damage done.
        // If that is the case, consider making a separate class to calculate damage, something like DamageCalculator.
        int clampedDamage = damage > Health.Current
            ? Health.Current
            : damage;
        
        Health.ChangeBy(-clampedDamage);
    }
}