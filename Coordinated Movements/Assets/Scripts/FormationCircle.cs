using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Math;
/*
 * A formation with fixed amount of slots representing a leader in the middle of a circle, 
 * with the followers spread of the circle at equal distance.
 * Followers will look outwards from the leader.
 * The whole formation turns together with the rotation of the leader.
 */
public class FormationCircle : MonoBehaviour, IAddRemoveAgents
{
    [SerializeField] protected int _nrOfSlots = 5;
    [SerializeField] protected float _distanceCenter = 5f;
    [SerializeField] protected float AngleOffset = 0f;
    [SerializeField] protected List<SteeringMovement> _agentsList;
    [SerializeField] MouseClickingBehavior _LeaderExternalSteering;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(!_LeaderExternalSteering) { return; }
        _LeaderExternalSteering.SetOnLeaderUpdateClb(OnLeaderUpdate);
    }

    protected virtual void Update()
    {
        // clean up any removed agents
        _agentsList = _agentsList.Where(agent => agent != null).ToList();
    }

    Vector2 slotToPosition(int slotNr, Vector2 goal, Vector2 direction)
    {
        // offset angle by the leader with up of screen being the default
        float leaderAngle = Vector2.SignedAngle(direction, Vector2.up) * Mathf.Deg2Rad;
        // set a unit on every angle.
        float anglePerSlot = 2 * Mathf.PI / _nrOfSlots;
        return goal + AngleToVec(anglePerSlot * slotNr + leaderAngle + AngleOffset) * _distanceCenter;
    }

    Vector2 slotToDirection(int slotNr, Vector2 goal, Vector2 direction)
    {
        return (slotToPosition(slotNr, goal, direction) - goal).normalized; 
    }
    void OnLeaderUpdate(Vector2 newGoal, Vector2 newDirection)
    {
        for(int slot = 0; slot < _nrOfSlots && slot < _agentsList.Count; ++slot)
        {
            _agentsList[slot].SetGoal(slotToPosition(slot, newGoal, newDirection), slotToDirection(slot, newGoal, newDirection));
        }
    }

    public virtual void AddAgent(MonoBehaviour agent)
    {
        // don't add if there are not any slots left
        if(_agentsList.Count >= _nrOfSlots) { return; }

        SteeringMovement sm = agent.GetComponent<SteeringMovement>();
        if (sm)
        {
            _agentsList.Add(sm);
        }
    }

    public virtual bool RemoveAgent(MonoBehaviour agent)
    {
        SteeringMovement sm = agent.GetComponent<SteeringMovement>();
        return sm != null && _agentsList.Remove(sm);
    }
}
