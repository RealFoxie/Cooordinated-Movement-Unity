using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Works just as Formation Circle, but with a variable amount of slots and a formation that will scale with them.
 */
public class FormationCircleScalable : FormationCircle
{
    [SerializeField] float _distanceBetweenAgents = 2.0f;


    protected override void Start()
    {
        base.Start();
        // make the amount of slots equal to amount of followers
        UpdateSlots();

    }

    protected override void Update()
    {
        base.Update();
        UpdateSlots();
    }

    //Update all the logic of slots so they scale. note that the placing of slots is already handeld by the class FormationCircle
    void UpdateSlots()
    {
        NrOfSlots = _agentsList.Count;
        float circumference = NrOfSlots * _distanceBetweenAgents;
        DistanceCenter = circumference / (2 * Mathf.PI);
    }

    public override void AddAgent(MonoBehaviour agent)
    {
        SteeringMovement sm = agent.GetComponent<SteeringMovement>();
        if (sm) {
            _agentsList.Add(sm);
            UpdateSlots();
        }
        
    }

    public override bool RemoveAgent(MonoBehaviour agent)
    {
        SteeringMovement sm = agent.GetComponent<SteeringMovement>();
        if (sm) {
            if (_agentsList.Remove(sm))
            {
                UpdateSlots();
                return true;
            }
        }
        return false;
    }
}
