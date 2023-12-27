using Ngin.Gameplay;

namespace Ngin.GameParticipants.AI.GOAP;

public abstract class Goal
{
    public abstract int GetPriority(Game game, GameParticipant gameParticipant);
    public abstract int GetScore(Game game, GameParticipant gameParticipant);
}