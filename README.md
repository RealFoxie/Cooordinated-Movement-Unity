# Coordinated Movement Formations In Unity
Implementing coordinated movement structures as explained in Ian Millington and John Funge's book *Artificial Intelligence For Games*.
## Introduction
When agents move in a formation, they will move based on a pattern and try to stay in that pattern to the best of their ability.
When the formation moves, often by moving the leader of a formation, the agents will follow to their new place, which is called a slot of the formation.  
Note that there are several possibilities to let the agents find their slot. One could limit the movement of the formation to make sure all agents stay close to their desired slot position, or one could move the formation at free will and let the agents themselves get to their slot at their own accord, ignoring if they are able to do so. All following implementations use the latter logic.

For every scene, use left click to move the leader, right click to delete an agent and press A to add one.

## Fixed Formation
![FixedFormation_Updated](https://user-images.githubusercontent.com/52831920/151002642-29a7219f-947e-4b9b-b692-f4568b71fe40.gif)  
For this type of formation, the amount of slots and the position of them (in the formation) is fixed. Agents choose a slot when entering the formation and stay part of that slot. If all slots are taken, no more agents can join the formation, which is shown at the end of the GIF.  
This particular formation is one with a leader in the middle of a circle, with slots spread across the circle at equal distances. The agents look outwards and the formation will turn together with the leader. This can be seen by following the green agent, which stays in front of the leader.  
To test this out in Unity, open the Scene *FixedFormationScene*.


## Scalable Formation
![ScalableFormation](https://user-images.githubusercontent.com/52831920/151004327-a741e837-f4bd-49f4-875b-03aac62805bf.gif)  
The base formation used is also a leader in the middle of a circle, with slots spread across the circle at equal distances. However, here the amount of slots and their position are dynamic. The slots are now defined so that every agent is a specific distance away from the rest. This means the circle has to increase in size when more agents join. Whenever an agent enters or leaves the formation, the slots are updated.  
To test this out in Unity, open the Scene *ScalableFormationScene*.

## Emergent Formation
![EmergentFormation](https://user-images.githubusercontent.com/52831920/151006695-75d41972-1337-4ad3-b177-2cf235c1f48d.gif)  
This formation works differently from the previous ones. There is no overal formation structure, instead every agent chooses another agent (its leader) to get a slot from and follows that agent around. This means that the formation is infinitely scalable. However, it also means that the formation can look vastly different depending on how the slots are chosen.  
A V-shaped formation is implemented, where every agent has two slots behind him. Pressing S shuffles the list of agents, changing the way they will search for a new slot.  
An infinite loop can occur if two agents choose a slot of each other (possibly with some proxy agents in between). To combat this problem, agents only have a valid slot if they already have a leader to follow. At the start, only the red agent has valid slots.  
A new agent will loop over all the agents and check if they have any valid slots. A slot is valid if it is free, the agent already follows a leader and the position of that slot is not yet taken by a different agent. This check is needed as some slots might overlap, but only one agent is allowed at a time at a position.  
When an agent is removed, it tells its leader that it will leave the occupied slot. It will also let the agents that occupy its slots know that they should leave. When an agent leaves a slot, it does not follow a leader anymore, which, as said previously, means it does not have any valid slots. Hence also that agent's slots have to be emptied.  
Hence, as shown in the GIF, if an agent close to the red leader is removed, all agents behind will search for a new slot. Of S was pressed to shuffle the list, this can make different patterns emerge.  
To test this out in Unity, open the Scene *EmergentFormationScene*.
