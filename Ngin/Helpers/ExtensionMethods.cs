using System;

namespace Ngin.Helpers;

public static class ExtensionMethods
{
    /// <summary>
    /// Returns value clamped to the inclusive range of min and max.
    /// </summary>
    public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
    {
        T clampedValue = value;

        if (value.CompareTo(min) < 0)
        {
            clampedValue = min;
        }
        else if (value.CompareTo(max) > 0)
        {
            clampedValue = max;
        }
        
        return clampedValue;
    }
}