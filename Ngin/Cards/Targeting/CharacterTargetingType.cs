using System.Collections.Generic;
using Ngin.Characters;
using Ngin.GameParticipants;

namespace Ngin.Cards.Targeting;

public class CharacterTargetingType : TargetingType<Character>
{
    protected CharacterTargetingType(TargetsFinder targetsFinder) : base(targetsFinder)
    {
    }
    
    /// <summary>
    /// Targets the user.
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
    /// Allows choosing any alive character, including the user.
    /// </summary>
    public static CharacterTargetingType Alive = new(user =>
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
    /// Allows choosing the user or any alive character that is controlled by the same <see cref="GameParticipant"/> as the user.
    /// </summary>
    public static CharacterTargetingType AliveAllyOrUser = new(user =>
    {
        List<TargetOption<Character>> targetOptions = new();
        
        for (int i = 0; i < user.Game.AllCharacters.Count; i++)
        {
            Character consideredCharacter = user.Game.AllCharacters[i];
            
            if (!consideredCharacter.IsDead && consideredCharacter.Owner == user.Owner)
            {
                TargetOption<Character> targetOption = new(consideredCharacter);
                targetOptions.Add(targetOption);
            }
        }

        return targetOptions;
    });
    
    /// <summary>
    /// Allows choosing any alive character that is controller by a different <see cref="GameParticipant"/> than the user.
    /// </summary>
    public static CharacterTargetingType AliveEnemy = new(user =>
    {
        List<TargetOption<Character>> targetOptions = new();
        
        for (int i = 0; i < user.Game.AllCharacters.Count; i++)
        {
            Character consideredCharacter = user.Game.AllCharacters[i];
            
            if (!consideredCharacter.IsDead && consideredCharacter.Owner != user.Owner)
            {
                TargetOption<Character> targetOption = new(consideredCharacter);
                targetOptions.Add(targetOption);
            }
        }

        return targetOptions;
    });
    
    /// <summary>
    /// Targets all alive characters that are controlled by a different <see cref="GameParticipant"/> than the user.
    /// </summary>
    public static CharacterTargetingType AllAliveEnemies = new(user =>
    {
        List<Character> aliveEnemyCharacters = new();
        
        for (int i = 0; i < user.Game.AllCharacters.Count; i++)
        {
            Character consideredCharacter = user.Game.AllCharacters[i];
            
            if (!consideredCharacter.IsDead && consideredCharacter.Owner != user.Owner)
            {
                aliveEnemyCharacters.Add(consideredCharacter);
            }
        }

        TargetOption<Character> targetOption = new(aliveEnemyCharacters.ToArray());
        return new List<TargetOption<Character>>{ targetOption };
    });
}