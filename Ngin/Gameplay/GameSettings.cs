namespace Ngin.Gameplay;

public readonly struct GameSettings
{
    public readonly int CardsAllowedToPlayPerTurn;
    public readonly int CardsToDrawOnGameStart;
    public readonly int CardsToDrawOnTurnStart;
    public readonly int MaxCardsInHand;

    public GameSettings(int cardsAllowedToPlayPerTurn, int cardsToDrawOnGameStart, int cardsToDrawOnTurnStart, int maxCardsInHand)
    {
        CardsAllowedToPlayPerTurn = cardsAllowedToPlayPerTurn;
        CardsToDrawOnGameStart = cardsToDrawOnGameStart;
        CardsToDrawOnTurnStart = cardsToDrawOnTurnStart;
        MaxCardsInHand = maxCardsInHand;
    }
}