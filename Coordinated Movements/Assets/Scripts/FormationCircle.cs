using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Math;
/*
 * A formation with fixed amount of slots representing a leader in the middle of a circle, 
 * with the followers spread of the circle at equal distance.
 * Followers will look outwards from the leader.
 * The whole formation turns together with the rotation of the leader.
 */
public class FormationCircle : MonoBehaviour
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
}
