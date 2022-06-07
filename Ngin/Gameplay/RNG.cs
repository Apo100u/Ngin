using System;

namespace Ngin.Gameplay;

public static class RNG
{
    private static Random random = new();

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