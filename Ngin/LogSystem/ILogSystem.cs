using System.Collections.Generic;

namespace Ngin.LogSystem;

public interface ILogSystem
{
    public void LogCardsToChooseFrom(Dictionary<string, string> cardNamesByKeyboardKeys, string passKey = null);
}