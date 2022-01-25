using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * Used for Emergent Formation. This will keep track of the group that should position themselves via Emergent logic.
 * Hence this does not include any Formation logic, but only the add/remove agent from the group.
 * 
 */
public class EmergentFormationGroup : MonoBehaviour, IAddRemoveAgents
{
    protected List<EmergentFormationVshape> _agentsList = new List<EmergentFormationVshape>();
    [SerializeField] float _minDistanceSlots = 2f;

    // Scramble list when pushing a key. Note: this is not really part of the formation
    // But for demonstration purposes
    void Update()
    {
        // New coordinates to move to
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Randomized list of Emergent Formation Group.");
            _agentsList.Shuffle();
        }
    }

    public void JoinGroup(EmergentFormationVshape agent)
    {
        _agentsList.Add(agent);
    }
    public bool LeaveGroup(EmergentFormationVshape agent)
    {
        return _agentsList.Remove(agent);
    }

    public List<EmergentFormationVshape> GetGroup()
    {
        return _agentsList;
    }

    // check if any agent in the group already has a claimed slot for this position.
    public bool IsSlotFree(Vector2 position)
    {
        foreach (EmergentFormationVshape agent in _agentsList)
        {
            if(agent.IsFollower() && agent.HasSlot() && Vector2.SqrMagnitude(agent.DesiredLocation() - position) < MinSqrDistance())
            {
                return false;
            }
        }
        return true;
    }

    float MinSqrDistance()
    {
        return _minDistanceSlots * _minDistanceSlots;
    }

    public void AddAgent(MonoBehaviour agent)
    {
        EmergentFormationVshape e = agent.GetComponent<EmergentFormationVshape>();
        if (e) { 
            JoinGroup(e);
            e.Group = this;
        }
    }

    public bool RemoveAgent(MonoBehaviour agent)
    {
        EmergentFormationVshape e = agent.GetComponent<EmergentFormationVshape>();
        if (e) { return LeaveGroup(e); }
        return false;
    }
}
