using System.Collections.Generic;
using Ngin.Gameplay;

namespace Ngin.GameParticipants.AI;

public class GameTreeNode
{
    public readonly Game Game;
    public int? ActionLeadingToThisNode;
    public GameTreeNode Parent;
    public List<GameTreeNode> Children = new();

    public GameTreeNode(Game value, GameTreeNode parent)
    {
        Game = value;
        Parent = parent;
    }

    public GameTreeNode AddChild(Game value)
    {
        GameTreeNode node = new(value, this);
        Children.Add(node);
        return node;
    }
}