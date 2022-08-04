using System;
using Ngin.Cards.Effects;

namespace Ngin.Cards;

public class Card
{
    public readonly string Name;

    private int currentEffectToPerformIndex;
    private Action onPlayed;
    private Action onCancelled;
    private CardEffect[] effects;

    public Card(string name, params CardEffect[] effects)
    {
        Name = name;
        this.effects = effects;
    }

    public void Play(Action onPlayed, Action onCancelled)
    {
        this.onPlayed = onPlayed;
        this.onCancelled = onCancelled;
        currentEffectToPerformIndex = 0;
        PerformNextEffect();
    }

    private void PerformNextEffect()
    {
        effects[currentEffectToPerformIndex].Perform(OnEffectPerformed, OnEffectCancelled);
    }

    private void OnEffectPerformed()
    {
        bool areAllEffectsPlayed = currentEffectToPerformIndex == effects.Length - 1;
        
        if (areAllEffectsPlayed)
        {
            OnAllEffectsPlayed();
        }
        else
        {
            currentEffectToPerformIndex++;
            PerformNextEffect();
        }
    }

    private void OnEffectCancelled()
    {
        onCancelled?.Invoke();
    }

    private void OnAllEffectsPlayed()
    {
        onPlayed?.Invoke();

        onPlayed = null;
        onCancelled = null;
    }
}