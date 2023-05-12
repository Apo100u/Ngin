using System;
using System.Collections.Generic;
using Ngin.Cards.Targeting;
using Ngin.Characters;

namespace Ngin.Cards.Effects;

public class Damage : CardEffect
{
    public readonly int Power;

    private Action onPerformed;
    private CharacterTargetingType targetingType;

    public Damage(int power, CharacterTargetingType targetingType)
    {
        Power = power;
        this.targetingType = targetingType;
    }

    public override string GetDescription()
    {
        throw new NotImplementedException();
    }

    public override void Perform(Character user, Action onPerformed, Action onCancelled)
    {
        this.onPerformed = onPerformed;
        
        List<TargetOption<Character>> targetOptions = targetingType.GetAvailableTargetOptions(user);
        
        user.Game.Input.StartNewChoice(user.Owner);
        user.Game.Input.AllowChoosingTargetsFromOptions(targetOptions, OnTargetOptionChosen);
        user.Game.Input.AllowCanceling(onCancelled);
    }

    private void OnTargetOptionChosen(TargetOption<Character> targetOption)
    {
        for (int i = 0; i < targetOption.Targets.Length; i++)
        {
            targetOption.Targets[i].ApplyDamage(this);
        }
        
        onPerformed?.Invoke();
    }
}