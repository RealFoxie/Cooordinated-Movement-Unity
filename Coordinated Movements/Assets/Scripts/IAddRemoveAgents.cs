using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddRemoveAgents
{
    void AddAgent(SteeringMovement agent);

    // return of the agent was present and removed
    bool RemoveAgent(SteeringMovement agent);
}
