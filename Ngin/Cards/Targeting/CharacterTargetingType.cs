using System.Collections.Generic;
using Ngin.Characters;

namespace Ngin.Cards.Targeting;

public class CharacterTargetingType : TargetingType<Character>
{
    protected CharacterTargetingType(TargetsFinder targetsFinder) : base(targetsFinder)
    {
    }

    public static CharacterTargetingType siema = new(user =>
    {
        return new List<TargetOption<Character>>();
    });
    
    public static CharacterTargetingType elo = new(user =>
    {
        return new List<TargetOption<Character>>();
    });
}