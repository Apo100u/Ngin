using System.Collections.Generic;
using Ngin.Cards;

namespace Ngin.LogSystem;

public interface ILogSystem
{
    public void LogInvalidInput(string invalidInput, List<string> validInputs);
    public void LogCardsToChooseFrom(Dictionary<string, Card> cardsByInput);
    public void LogCardChoice(Card chosenCard);
    public void LogPassInput(string passInput);
}