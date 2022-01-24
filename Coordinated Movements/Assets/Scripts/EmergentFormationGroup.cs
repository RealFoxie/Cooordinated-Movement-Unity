using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used for Emergent Formation. This will keep track of the group that should position themselves via Emergent logic.
 * Hence this does not include any Formation logic, but only the add/remove agent from the group.
 * 
 */
public class EmergentFormationGroup : MonoBehaviour
{
    protected List<EmergentFormationVshape> _agentsList = new List<EmergentFormationVshape>();
    [SerializeField] float _minDistanceSlots = 2f;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinGroup(EmergentFormationVshape agent)
    {
        _agentsList.Add(agent);
    }
    public void LeaveGroup(EmergentFormationVshape agent)
    {
        _agentsList.Remove(agent);
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

}
