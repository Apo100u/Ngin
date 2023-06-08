using System.Collections.Generic;
using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.InputSystem.Actions;
using Ngin.ThirdParty;

namespace Ngin.GameParticipants.AI;

public class TestAi : GameParticipant
{
    private Stack<int> indexesOfActionsToExecute = new(); // REFACTOR: This should be a queue. It's now a stack because of how the actions are calculated, but it should be remade into queue to be more intuitive.

    public TestAi(Game game, string name, params Character[] ownedCharacters) : base(game, name, ownedCharacters)
    {
    }

    public override void ChooseAction()
    {
        if (indexesOfActionsToExecute.Count > 0)
        {
            ExecuteNextAction();
        }
        else
        {
            indexesOfActionsToExecute = CalculateNextActions();
        }
    }

    private void ExecuteNextAction()
    {
        int chosenActionIndex = indexesOfActionsToExecute.Pop();
        Game.Input.AllowedActions[chosenActionIndex].Execute();
    }

    private Stack<int> CalculateNextActions()
    {
        Game copiedGame = Game.DeepCopy();
        GameTreeNode searchTree = new(copiedGame, null);
        copiedGame.Log.StopLogging();

        GameTreeNode nodeWithBestScore = FindNodeWithBestScore(searchTree);

        Stack<int> indexesOfCalculatedActions = new();
        GameTreeNode nextNodeInChosenTreePath = nodeWithBestScore;

        while (nextNodeInChosenTreePath?.ActionLeadingToThisNode != null)
        {
            indexesOfCalculatedActions.Push((int)nextNodeInChosenTreePath.ActionLeadingToThisNode);
            nextNodeInChosenTreePath = nextNodeInChosenTreePath.Parent;
        }

        return indexesOfCalculatedActions;
    }

    private GameTreeNode FindNodeWithBestScore(GameTreeNode searchTree)
    {
        Queue<GameTreeNode> nodesToExpand = new();
        GameTreeNode nodeWithBestScore = searchTree;
        int currentBestScore = int.MinValue;
        
        nodesToExpand.Enqueue(searchTree);
        
        while (nodesToExpand.Count > 0)
        {
            GameTreeNode nodeToExpand = nodesToExpand.Dequeue();
            ExpandNode(nodeToExpand, nodesToExpand, ref nodeWithBestScore, ref currentBestScore);
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
                    int score = Heuristics.GetParticipantsScoreBasedOnHealth(expandedCopy, thisParticipantInCopiedGame);

                    if (score > currentBestScore)
                    {
                        currentBestScore = score;
                        nodeWithBestScore = nodeWithActionOutcome;
                    }
                }
            }
        }
    }
}