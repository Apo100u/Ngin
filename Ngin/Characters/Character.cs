using Ngin.Cards;

namespace Ngin.Characters;

public class Character
{
    public readonly string Name;
    public readonly Statistic Health;
    public readonly Statistic Initiative;
    public readonly CardSet Hand = new();
    public readonly CardSet Deck = new();

    public Character(string name, int baseHealth, int baseInitiative)
    {
        Name = name;
        Health = new Statistic(baseHealth);
        Initiative = new Statistic(baseInitiative);
    }

    public void Draw(int amount)
    {
    }
}