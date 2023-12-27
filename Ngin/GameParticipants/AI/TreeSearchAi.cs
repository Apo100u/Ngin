using System.Collections.Generic;
using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.InputSystem.Actions;
using Ngin.ThirdParty;

namespace Ngin.GameParticipants.AI;

public class TreeSearchAi : GameParticipant
{
    private Queue<int> indexesOfActionsToExecute = new();
    
    protected bool AnyActionsQueued => indexesOfActionsToExecute.Count > 0;

    public TreeSearchAi(Game game, string name, params Character[] ownedCharacters) : base(game, name, ownedCharacters)
    {
    }

    public override void ChooseAction()
    {
        if (AnyActionsQueued)
        {
            ExecuteNextAction();
        }
        else
        {
            CalculateNextActions();
        }
    }

    private void ExecuteNextAction()
    {
        int chosenActionIndex = indexesOfActionsToExecute.Dequeue();
        Game.Input.AllowedActions[chosenActionIndex].Execute();
    }

    private void CalculateNextActions()
    {
        Game copiedGame = Game.DeepCopy();
        GameTreeNode searchTree = new(copiedGame, null);
        copiedGame.Log.StopLogging();

        GameTreeNode nodeWithBestScore = FindNodeWithBestScore(searchTree);

        indexesOfActionsToExecute = GetIndexesOfActionsToExecuteFromNode(nodeWithBestScore);
    }

    private GameTreeNode FindNodeWithBestScore(GameTreeNode searchTree)
    {
        Queue<GameTreeNode> nodesToExpand = new();
        GameTreeNode nodeWithBestScore = searchTree;
        int currentBestScore = int.MinValue;
        
        nodesToExpand.Enqueue(searchTree);
        
        while (nodesToExpand.Count > 0)
        {
            GameTreeNode expandedNode = nodesToExpand.Dequeue();
            ExpandNode(expandedNode, nodesToExpand, ref nodeWithBestScore, ref currentBestScore);
        }

        return nodeWithBestScore;
    }

    private void ExpandNode(GameTreeNode expandedNode, Queue<GameTreeNode> nodesToExpand, ref GameTreeNode nodeWithBestScore, ref int currentBestScore)
    {
        for (int i = 0; i < expandedNode.Game.Input.AllowedActions.Count; i++) // OPTIMIZE: If multiple actions are the same, don't check all of them.
        {
            if (expandedNode.Game.Input.AllowedActions[i] is not CancelAction)
            {
                Game expandedCopy = expandedNode.Game.DeepCopy();
                GameParticipant thisParticipantInCopiedGame = expandedCopy.Input.ParticipantChoosingAction;
                expandedCopy.Input.AllowedActions[i].Execute();
                GameTreeNode nodeWithActionOutcome = expandedNode.AddChild(expandedCopy);
                nodeWithActionOutcome.ActionLeadingToThisNode = i;

                if (expandedCopy.Input.ParticipantChoosingAction == thisParticipantInCopiedGame && !expandedCopy.IsFinished)
                {
                    nodesToExpand.Enqueue(nodeWithActionOutcome);
                }
                else
                {
                    int score = GetParticipantsScore(expandedCopy, thisParticipantInCopiedGame);

                    if (score > currentBestScore)
                    {
                        currentBestScore = score;
                        nodeWithBestScore = nodeWithActionOutcome;
                    }
                }
            }
        }
    }

    protected virtual int GetParticipantsScore(Game game, GameParticipant gameParticipant)
    {
        return Heuristics.GetScoreBasedOnHealth(game, gameParticipant);
    }
    
    private Queue<int> GetIndexesOfActionsToExecuteFromNode(GameTreeNode node)
    {
        List<int> indexesOfActionsPlayed = new();
        GameTreeNode nextNodeInChosenTreePath = node;

        while (nextNodeInChosenTreePath?.ActionLeadingToThisNode != null)
        {
            indexesOfActionsPlayed.Add((int)nextNodeInChosenTreePath.ActionLeadingToThisNode);
            nextNodeInChosenTreePath = nextNodeInChosenTreePath.Parent;
        }

        Queue<int> indexesOfActionsToExecuteInOrder = new();

        for (int i = indexesOfActionsPlayed.Count - 1; i >= 0; i--)
        {
            indexesOfActionsToExecuteInOrder.Enqueue(indexesOfActionsPlayed[i]);
        }
        
        return indexesOfActionsToExecuteInOrder;
    }
}