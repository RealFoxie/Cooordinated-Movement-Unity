using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringMovement))]
public class EmergentFormationVshape : MonoBehaviour
{
    EmergentFormationVshape _agentToFollow;
    SteeringMovement _steering;
    EmergentFormationVshape _leftAgent;
    EmergentFormationVshape _rightAgent;
    [SerializeField] EmergentFormationGroup _group;
    [SerializeField] bool _follow = true;
    [SerializeField] float _slotOffset = 1f;

    enum SlotPos
    {
        left,
        right
    }
    // Start is called before the first frame update
    private void Awake()
    {
        _steering = GetComponent<SteeringMovement>();
        _steering.SetOnGoalChangeClb(UpdateSlotsGoal);
        if (!_group)
        {
            _group = GetComponentInParent<EmergentFormationGroup>();
        }
        _group.JoinGroup(this);
    }
    void Start()
    {
    }

    
    public void FindSlot()
    {
        if (!_follow) { return; }
        foreach(EmergentFormationVshape agent in _group.GetGroup())
        {
            if (agent != this && agent.GetSlot(this))
            {
                _agentToFollow = agent;
                UpdateGoal();
                return;
            }
        }
        Debug.LogWarning("There was no free slot in the group for this agent.");
    }

    bool GetSlot(EmergentFormationVshape agent)
    {
        // can only assign a slot if itself has a slot (or if it is a leader and doesn't follow a slot). 
        // Otherwise a loop of slot assignation might happen.
        if(!HasSlot() && IsFollower()) { return false; }
        if (!_leftAgent)
        {
            if (_group.IsSlotFree(GetSlotPosition(SlotPos.left)))
            {
                _leftAgent = agent;
                return true;
            }
        }
        if (!_rightAgent)
        {
            if (_group.IsSlotFree(GetSlotPosition(SlotPos.right)))
            {
                _rightAgent = agent;
                return true;
            }
        }
        return false;
    }

    // reset the goal based on the slot this agent uses
    public void UpdateGoal()
    {
        if (HasSlot())
        {
            _steering.SetGoal(_agentToFollow.GetSlotPosition(this), _agentToFollow.GetSlotDirection(this));
        }
    }

    void UpdateSlotsGoal(Vector2 position, Vector2 direction)
    {
        if(_leftAgent) { _leftAgent.UpdateGoal(); }
        if (_rightAgent) { _rightAgent.UpdateGoal(); }
    }

    // always try to find a slot
    void Update()
    {
        if (!_follow) { return; }

        if (!HasSlot())
        {
            FindSlot();
        }
    }

    Vector2 GetSlotPosition(SlotPos slot)
    {
        Vector2 desiredDirection = DesiredDirection();
        Vector2 offsetDown = -desiredDirection * _slotOffset;
        Vector2 offsetLeft = Vector2.Perpendicular(desiredDirection) * _slotOffset;
        if (slot == SlotPos.left)
        {

            return DesiredLocation() + offsetDown + offsetLeft;
        }
        else
        {
            return DesiredLocation() + offsetDown - offsetLeft;
        }
    }
    public Vector2 GetSlotDirection(MonoBehaviour agent)
    {
        return DesiredDirection();
    }
    public Vector2 GetSlotPosition(MonoBehaviour agent)
    {
        if (_leftAgent == agent)
        {
            return GetSlotPosition(SlotPos.left);
        }
        if(_rightAgent == agent)
        {
            return GetSlotPosition(SlotPos.right);
        }
        Debug.LogError("Agent is not part of the EmergentFormation.");
        return new Vector2(0,0);
    }

    public bool HasSlot()
    {
        return _agentToFollow != null;
    }
    public bool IsFollower()
    {
        return _follow;
    }
    // return the location this agent is walking to.
    public Vector2 DesiredLocation()
    {
        return _steering.Goal;
    }

    // return the direction this agent is turning to.
    public Vector2 DesiredDirection()
    {
        return _steering.Direction;
    }
}
