using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Works just as Formation Circle, but with a variable amount of slots and a formation that will scale with them.
 */
public class FormationCircleScalable : FormationCircle, IAddRemoveAgents
{
    [SerializeField] float _distanceBetweenAgents = 2.0f;


    protected override void Start()
    {
        base.Start();
        // make the amount of slots equal to amount of followers
        _nrOfSlots = _agentsList.Count;
    }

    void Update()
    {
        // clean up any removed agents
        _agentsList = _agentsList.Where(agent => agent != null).ToList();
    }

    //Update all the logic of slots so they scale. note that the placing of slots is already handeld by the class FormationCircle
    void UpdateSlots()
    {
        _nrOfSlots = _agentsList.Count;
        float circumference = _nrOfSlots * _distanceBetweenAgents;
        _distanceCenter = circumference / (2 * Mathf.PI);
    }

    public void AddAgent(SteeringMovement agent)
    {
        _agentsList.Add(agent);
        UpdateSlots();
    }

    public bool RemoveAgent(SteeringMovement agent)
    {
        if (_agentsList.Remove(agent))
        {
            UpdateSlots();
            return true;
        }
        return false;
    }
}
