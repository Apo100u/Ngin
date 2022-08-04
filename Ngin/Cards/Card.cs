using System;
using Ngin.Cards.Effects;
using Ngin.Characters;

namespace Ngin.Cards;

public class Card
{
    public readonly string Name;
    public readonly Character Owner;

    private int currentEffectToPerformIndex;
    private Action onPlayed;
    private Action onCancelled;
    private CardEffect[] effects;

    public Card(Character owner, string name, params CardEffect[] effects)
    {
        Name = name;
        Owner = owner;
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