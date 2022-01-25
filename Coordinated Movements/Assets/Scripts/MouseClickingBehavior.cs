using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Simple script that will move a leader agent with a SteeringMovement to the mouse click location
 * and view direction towards the mouse.
 * Attach to camera
 */
[RequireComponent(typeof(Camera))]
public class MouseClickingBehavior : MonoBehaviour
{
    Camera _mainCamera;
    [SerializeField] SteeringMovement _leader;
    System.Action<Vector2, Vector2> _OnLeaderUpdate;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }
    private void Update()
    {
        // New coordinates to move to
        if (Input.GetMouseButton(0) && _mainCamera && _leader)
        {
            Vector2 newGoal = _mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
            Vector2 direction = newGoal - (Vector2)_leader.transform.position;
            _leader.SetGoal(newGoal, direction);
            if (_OnLeaderUpdate != null)
            {
                _OnLeaderUpdate(newGoal, direction);
            }
        }
    }

    public void SetOnLeaderUpdateClb(System.Action<Vector2, Vector2> clb)
    {
        _OnLeaderUpdate = clb;
        _OnLeaderUpdate(_leader.Goal, _leader.Direction);
    }
}


