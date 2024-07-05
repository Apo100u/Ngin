# About project
Console application that represents backend of a made-up card game with rules loosely inspired by Slay the Spire and Darkest Dungeon.

The goal of the project was to implement playing against enemy AI that uses Tree-Search and GOAP (Goal Oriented Action Planning) algorithms to find the best moves.

![GameStateExample](ReadmeFiles/GameStateExample.PNG)

### Notable classes:
* [TreeSearchAi](Ngin/GameParticipants/AI/TreeSearchAi.cs)
* [Heuristics](Ngin/GameParticipants/AI/Heuristics.cs)
* [Character](Ngin/Characters/Character.cs)

# Improvement possibilities
The application is a proof-of-concept and satisfies the goal of learning more about enemy AI in turn-based and/or card games. However, to make it more 
interesting and to really test the possibilities and limits of the implemented enemy AI, more different cards and card effects could be added.

# Architecture
Project architecture is presented on the following images.
### Components
![Gameplay](ReadmeFiles/Gameplay.png)
![Cards](ReadmeFiles/Cards.png)
![Input System](ReadmeFiles/Input%20System.png)
![Components Hierarchy](ReadmeFiles/Components%20Hierarchy.png)

### Program Loop
![Program Loop 1](ReadmeFiles/Program%20Loop%201.png)
![Program Loop 2](ReadmeFiles/Program%20Loop%202.png)
![Program Loop 3](ReadmeFiles/Program%20Loop%203.png)
