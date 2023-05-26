using System.Collections.Generic;
using Ngin.Characters;
using Ngin.Gameplay;
using Ngin.InputSystem.Actions;
using Ngin.ThirdParty;

namespace Ngin.GameParticipants.AI;

public class TestAi : GameParticipant
{
    private Stack<int> actionsToExecute = new();

    public TestAi(Game game, string name, params Character[] ownedCharacters) : base(game, name, ownedCharacters)
    {
    }
    
    private class GameTreeNode // In the future maybe move it to proper place, with proper naming, formatting etc? For now only for testing.
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

    public override void ChooseAction()
    {
        // TODO: This whole method needs hardcore refactor...

        if (actionsToExecute.Count > 0)
        {
            int chosenActionIndex = actionsToExecute.Pop();
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
                
                if (expandedCopy.Input.ParticipantChoosingAction == thisParticipantInCopiedGame)
                {
                    nodesToExpand.Enqueue(nodeWithActionOutcome);
                }
                else
                {
                    int score = TEST_GetTestHeuristicScoreForParticipant(expandedCopy, thisParticipantInCopiedGame);

                    if (score > currentBestScore)
                    {
                        currentBestScore = score;
                        nodeWithBestScore = nodeWithActionOutcome;
                    }
                }
            }
        }

        actionsToExecute = new Stack<int>();
        GameTreeNode nextNodeInChosenTreePath = nodeWithBestScore;

        while (nextNodeInChosenTreePath?.ActionLeadingToThisNode != null)
        {
            actionsToExecute.Push((int)nextNodeInChosenTreePath.ActionLeadingToThisNode);
            nextNodeInChosenTreePath = nextNodeInChosenTreePath.Parent;
        }
    }

    private int TEST_GetTestHeuristicScoreForParticipant(Game game, GameParticipant gameParticipant)
    {
        int participantScore = 0;
        int enemiesScore = 0;

        for (int i = 0; i < game.AllCharacters.Count; i++)
        {
            if (game.AllCharacters[i].Owner == gameParticipant)
            {
                participantScore += game.AllCharacters[i].Health.Current;

                if (game.AllCharacters[i].IsDead)
                {
                    participantScore -= 10;
                }
            }
            else
            {
                enemiesScore += game.AllCharacters[i].Health.Current;
                
                if (game.AllCharacters[i].IsDead)
                {
                    enemiesScore -= 10;
                }
            }
        }

        int test_veryNaiveHeuristicScore = participantScore - enemiesScore;
        return test_veryNaiveHeuristicScore;
    }
}