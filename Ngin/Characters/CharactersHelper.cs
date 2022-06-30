using System;
using Ngin.Gameplay;

namespace Ngin.Characters;

public static class CharactersHelper
{
    /// <summary>
    /// Returns a delegate to compare two characters based on their initiative. The delegate randomizes outcome if they have the same initiative.
    /// </summary>
    public static Comparison<Character> InitiativeComparisonWithRandomOnEqual()
    {
        return delegate(Character character1, Character character2)
        {
            int comparison;
            
            if (character1.Initiative.Current > character2.Initiative.Current)
            {
                comparison = 1;
            }
            else if (character1.Initiative.Current < character2.Initiative.Current)
            {
                comparison = -1;
            }
            else
            {
                comparison = RNG.NextSign();
            }

            return comparison;
        };
    }
}