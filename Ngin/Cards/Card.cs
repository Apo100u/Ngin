using Ngin.Cards.Effects;

namespace Ngin.Cards;

public class Card
{
    public readonly string Name;
        
    private CardEffect[] effects;

    public Card(string name, params CardEffect[] effects)
    {
        Name = name;
        this.effects = effects;
    }

    public void Play()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].Perform();
        }
    }
}