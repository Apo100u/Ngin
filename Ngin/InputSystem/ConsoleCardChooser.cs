using System;
using System.Collections.Generic;
using Ngin.Cards;
using Ngin.Gameplay;

namespace Ngin.InputSystem;

public class ConsoleCardChooser : CardChooser
{
    private static readonly string passInput = "pass";
    
    public ConsoleCardChooser(CardSet cardSetToChooseFrom, Action<CardsChosenEventArgs> onCardsChosen) : base(cardSetToChooseFrom, onCardsChosen)
    {
    }

    protected override Card ChooseNextCard(out bool passRequested)
    {
        Dictionary<string, Card> cardsByInput = GetCardsByInput(cardSetToChooseFrom);
        List<string> validInputs = new(cardsByInput.Keys);

        Game.LogSystem.LogCardsToChooseFrom(cardsByInput);

        if (CanPassNow())
        {
            Game.LogSystem.LogPassInput(passInput);
            validInputs.Add(passInput);
        }
        
        string userInput = GetValidUserInput(validInputs);

        passRequested = userInput.Equals(passInput);
        Card chosenCard = passRequested
            ? null
            : cardsByInput[userInput];

        return chosenCard;
    }

    private string GetValidUserInput(List<string> validInputs)
    {
        string userInput = string.Empty;
        
        while (!validInputs.Contains(userInput))
        {
            userInput = Console.ReadLine() ?? string.Empty;

            if (!validInputs.Contains(userInput))
            {
                Game.LogSystem.LogInvalidInput(userInput, validInputs);
            }
        }

        return userInput;
    }
    
    public Dictionary<string, Card> GetCardsByInput(CardSet cardSet)
    {
        Dictionary<string, Card> cardsByInput = new();

        for (int i = 0; i < cardSet.Count; i++)
        {
            string input = (i + 1).ToString();
            Card card = cardSet[i];
            cardsByInput.Add(input, card);
        }

        return cardsByInput;
    }
}