using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Math;

public class FormationCircle : MonoBehaviour
{
    [SerializeField] int _nrOfSlots = 5;
    [SerializeField] float _distanceCenter = 5f;
    [SerializeField] float AngleOffset = 5f;
    [SerializeField] List<SteeringMovement> _unitsList;
    [SerializeField] MouseMovement _leader;

    // Start is called before the first frame update
    void Start()
    {
        _leader.SetOnLeaderUpdateClb(OnLeaderUpdate);
    }

    Vector2 slotToPosition(int slotNr, Vector2 goal, Vector2 direction)
    {
        // offset angle by the leader with up of screen being the default
        float leaderAngle = Vector2.SignedAngle(direction, Vector2.up) * Mathf.Deg2Rad;
        // set a unit on every angle.
        float anglePerSlot = 2 * Mathf.PI / _nrOfSlots;
        return goal + AngleToVec(anglePerSlot * slotNr + leaderAngle) * _distanceCenter;
    }

    Vector2 slotToDirection(int slotNr, Vector2 goal, Vector2 direction)
    {
        return (slotToPosition(slotNr, goal, direction) - goal).normalized; 
    }
    void OnLeaderUpdate(Vector2 newGoal, Vector2 newDirection)
    {
        for(int slot = 0; slot < _nrOfSlots && slot < _unitsList.Count; ++slot)
        {
            _unitsList[slot].SetGoal(slotToPosition(slot, newGoal, newDirection), slotToDirection(slot, newGoal, newDirection));
        }
    }
}
