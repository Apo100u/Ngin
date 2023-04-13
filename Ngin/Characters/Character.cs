using Ngin.Cards;
using Ngin.Cards.Effects;
using Ngin.Gameplay;
using Ngin.Helpers;

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

    public void ApplyDamage(Damage damage)
    {
        int damagePower = new DamageCalculator(this, damage).CalculateDamage();
        Health.ChangeBy(-damagePower);
    }

    public void ApplyHeal(Heal heal)
    {
        int healPower = new HealCalculator(this, heal).CalculateHeal();
        Health.ChangeBy(healPower);
    }
}