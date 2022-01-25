using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A simple behavior that will add a defined agent at the point of the mouse when clicking.
 * It also accepts any classes implementing IAddRemoveAgents, to add this new agent.
 * Attach to camera
 */
[RequireComponent(typeof(Camera))]
public class AddOnKeyPress : MonoBehaviour
{
    Camera _mainCamera;
    [SerializeField] List<MonoBehaviour> _groupsList;
    List<IAddRemoveAgents> _addAgentToList = new List<IAddRemoveAgents>();
    [SerializeField] MonoBehaviour _agentToAdd;
    [SerializeField] KeyCode _key = KeyCode.A;
    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
        foreach (MonoBehaviour group in _groupsList)
        {
            IAddRemoveAgents i = group.GetComponent<IAddRemoveAgents>();
            if (i != null) {
                _addAgentToList.Add(i); 
            }
        }
    }
    private void Update()
    {
        // New coordinates to move to
        if (Input.GetKeyDown(_key) && _mainCamera && _agentToAdd)
        {
            Vector2 spawnLocation = _mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
            MonoBehaviour newAgent = Instantiate(_agentToAdd);
            newAgent.transform.position = spawnLocation;
            foreach(IAddRemoveAgents group in _addAgentToList)
            {
                group.AddAgent(newAgent);
            }
        }
    }
}
