using System;
using System.Collections.Generic;
using Ngin.Characters;
using Ngin.Gameplay;

namespace Ngin.InputSystem;

public class ConsoleInputSystem : IInputSystem
{
    public void ChooseCardFromHand(Character character, Action<CardChosenEventArgs> onCardChosen, bool allowPassing)
    {
        Dictionary<string, string> cardNamesByKeyboardKeys = new();

        for (int i = 0; i < character.Hand.Cards.Count; i++)
        {
            string keyboardKeyForThisCard = (i + 1).ToString();
            cardNamesByKeyboardKeys.Add(keyboardKeyForThisCard, character.Hand.Cards[i].Name);
        }

        Game.LogSystem.LogCardsToChooseFrom(cardNamesByKeyboardKeys, ConsoleKey.Backspace.ToString());
    }
}