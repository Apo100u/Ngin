using System;
using System.Collections.Generic;
using Ngin.Cards;
using Ngin.Gameplay;

namespace Ngin.InputSystem;

public abstract class CardChooser
{
    protected List<Card> chosenCards = new();
    protected CardSet cardSetToChooseFrom;
    protected Action<CardsChosenEventArgs> onCardsChosen;
    protected int minAmount;
    protected int maxAmount;
    
    private bool isPassingAllowed;
    private bool isPassRequested;

    protected abstract Card ChooseNextCard(out bool passRequested);

    protected CardChooser(CardSet cardSetToChooseFrom, Action<CardsChosenEventArgs> onCardsChosen)
    {
        this.cardSetToChooseFrom = cardSetToChooseFrom;
        this.onCardsChosen = onCardsChosen;
    }

    public void Start()
    {
        while (!IsChoosingDone())
        {
            Card chosenCard = ChooseNextCard(out isPassRequested);

            if (!isPassRequested)
            {
                Game.LogSystem.LogCardChoice(chosenCard);
                chosenCards.Add(chosenCard);
            }
        }
        
        onCardsChosen?.Invoke(new CardsChosenEventArgs(chosenCards, isPassRequested));
    }

    private bool IsChoosingDone()
    {
        return chosenCards.Count == maxAmount || isPassRequested;
    }

    protected bool CanPassNow()
    {
        return isPassingAllowed && chosenCards.Count >= minAmount;
    }

    public CardChooser AllowPassing()
    {
        isPassingAllowed = true;
        return this;
    }

    public CardChooser SetMinAmount(int minAmount)
    {
        this.minAmount = minAmount;
        return this;
    }
    
    public CardChooser SetMaxAmount(int maxAmount)
    {
        this.maxAmount = maxAmount;
        return this;
    }
}