using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddRemoveAgents
{
    void AddAgent(MonoBehaviour agent);

    // return of the agent was present and removed
    bool RemoveAgent(MonoBehaviour agent);
}
