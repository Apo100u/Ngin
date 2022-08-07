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
    
    public Character(Game game, string name, int baseHealth, int baseInitiative)
    {
        Game = game;
        Name = name;
        Health = new Statistic(baseHealth);
        Initiative = new Statistic(baseInitiative);
        IsDead = false;
    }


    public void Draw(int amount)
    {
    }
}