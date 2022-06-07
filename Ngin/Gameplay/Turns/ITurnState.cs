using System;

namespace Ngin.Gameplay.Turns;

public interface ITurnState
{
    public event Action Ended;
    public void Start();
}