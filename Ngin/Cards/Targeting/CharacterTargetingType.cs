using System.Collections.Generic;
using Ngin.Characters;

namespace Ngin.Cards.Targeting;

public class CharacterTargetingType : TargetingType<Character>
{
    protected CharacterTargetingType(TargetsFinder targetsFinder) : base(targetsFinder)
    {
    }
    
    /// <summary>
    /// Targets the character that is the user.
    /// </summary>
    public static CharacterTargetingType User = new(user =>
    {
        List<TargetOption<Character>> targetOptions = new()
        {
            new TargetOption<Character>(user)
        };
        
        return targetOptions;
    });

    /// <summary>
    /// Allows choosing any alive character, including allies and self.
    /// </summary>
    public static CharacterTargetingType AliveCharacter = new(user =>
    {
        List<TargetOption<Character>> targetOptions = new();
        
        for (int i = 0; i < user.Game.AllCharacters.Count; i++)
        {
            Character consideredCharacter = user.Game.AllCharacters[i];
            
            if (!consideredCharacter.IsDead)
            {
                TargetOption<Character> targetOption = new(consideredCharacter);
                targetOptions.Add(targetOption);
            }
        }

        return targetOptions;
    });
    
    /// <summary>
    /// Targets all alive characters that have a different <see cref="Team"/> than the user.
    /// </summary>
    public static CharacterTargetingType AllAliveEnemyCharacters = new(user =>
    {
        List<Character> aliveEnemyCharacters = new();
        
        for (int i = 0; i < user.Game.AllCharacters.Count; i++)
        {
            Character consideredCharacter = user.Game.AllCharacters[i];
            
            if (!consideredCharacter.IsDead && consideredCharacter.Team != user.Team)
            {
                aliveEnemyCharacters.Add(consideredCharacter);
            }
        }

        TargetOption<Character> targetOption = new(aliveEnemyCharacters.ToArray());
        return new List<TargetOption<Character>>{ targetOption };
    });
}