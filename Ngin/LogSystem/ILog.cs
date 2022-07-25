using Ngin.Characters;

namespace Ngin.LogSystem;

public interface ILog
{
    void TurnStart(int turnNumber);
    void TurnEnd(int turnNumber);
    void CharacterMoveStart(Character characterOnMove);
}