using System;
using System.Collections.Generic;
using Ngin.Cards.Targeting;
using Ngin.Characters;

namespace Ngin.Cards.Effects;

public class Draw : CardEffect
{
    public readonly int Count;
    
    private Action onPerformed;
    private CharacterTargetingType targetingType;

    public Draw(int count, CharacterTargetingType targetingType)
    {
        Count = count;
        this.targetingType = targetingType;
    }

    public override string GetDescription()
    {
        throw new System.NotImplementedException();
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
        for (int i = 0; i < targetOption.Targets.Count; i++)
        {
            targetOption.Targets[i].ApplyDraw(this);
        }
        
        onPerformed?.Invoke();
    }
}