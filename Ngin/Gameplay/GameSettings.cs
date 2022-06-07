namespace Ngin.Gameplay;

public readonly struct GameSettings
{
    public readonly int CardsToDrawOnGameStart;
    public readonly int CardsToDrawOnTurnStart;
    public readonly int MaxCardsInHand;

    public GameSettings(int cardsToDrawOnGameStart, int cardsToDrawOnTurnStart, int maxCardsInHand)
    {
        CardsToDrawOnGameStart = cardsToDrawOnGameStart;
        CardsToDrawOnTurnStart = cardsToDrawOnTurnStart;
        MaxCardsInHand = maxCardsInHand;
    }
}