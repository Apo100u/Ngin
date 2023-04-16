using Ngin.Cards.Effects;
using Ngin.Characters;

namespace Ngin.Helpers.Calculators;

public class DrawCalculator
{
    private readonly Character drawingCharacter;
    private readonly Draw draw;

    public DrawCalculator(Character drawingCharacter, Draw draw)
    {
        this.drawingCharacter = drawingCharacter;
        this.draw = draw;
    }

    public int CalculateCardsDrawnCount()
    {
        int cardsDrawnCount = draw.Count;

        if (cardsDrawnCount < 0)
        {
            cardsDrawnCount = 0;
        }

        return cardsDrawnCount;
    }
}