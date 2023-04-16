using System;
using Ngin.Characters;

namespace Ngin.Gameplay;

public static class RNG
{
    private static Random random = new();

    /// <summary>
    /// Returns a delegate to compare two characters based on their initiative. The delegate randomizes the outcome if they have the same initiative.
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
                comparison = NextSign();
            }

            return comparison;
        };
    }
    
    /// <summary>
    /// Randomly returns -1 or 1.
    /// </summary>
    public static int NextSign()
    {
        return random.NextDouble() > 0.5
            ? 1
            : -1;
    }
}