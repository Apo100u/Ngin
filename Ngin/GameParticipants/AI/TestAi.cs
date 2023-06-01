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
        // TODO: This whole method needs hardcore refactor...

        if (indexesOfActionsToExecute.Count > 0)
        {
            int chosenActionIndex = indexesOfActionsToExecute.Pop();
            Game.Input.AllowedActions[chosenActionIndex].Execute();
            return; // REFACTOR: NO RETURN IN THE MIDDLE OF THE CODE!!!
        }
        
        Game copiedGame = Game.DeepCopy();
        copiedGame.Log.StopLogging();
        GameTreeNode searchTree = new(copiedGame, null);

        int currentBestScore = int.MinValue;
        GameTreeNode nodeWithBestScore = searchTree;
        
        Queue<GameTreeNode> nodesToExpand = new();
        nodesToExpand.Enqueue(searchTree);

        while (nodesToExpand.Count > 0) // REFACTOR: Split into methods for readability
        {
            GameTreeNode expandedNode = nodesToExpand.Dequeue();

            for (int i = 0; i < expandedNode.Game.Input.AllowedActions.Count; i++) // OPTIMIZE: If multiple actions are the same, don't check all of them.
            {
                if (expandedNode.Game.Input.AllowedActions[i] is CancelAction)
                {
                    continue; // REFACTOR: NO CONTINUE IN THE MIDDLE OF THE CODE!!!
                }

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

        indexesOfActionsToExecute = new Stack<int>();
        GameTreeNode nextNodeInChosenTreePath = nodeWithBestScore;

        while (nextNodeInChosenTreePath?.ActionLeadingToThisNode != null)
        {
            indexesOfActionsToExecute.Push((int)nextNodeInChosenTreePath.ActionLeadingToThisNode);
            nextNodeInChosenTreePath = nextNodeInChosenTreePath.Parent;
        }
    }
}