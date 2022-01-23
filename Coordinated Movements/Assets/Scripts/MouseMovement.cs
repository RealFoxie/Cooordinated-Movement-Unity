using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] SteeringMovement _leader;
    System.Action<Vector2, Vector2> _OnLeaderUpdate;

    private void Update()
    {
        // New coordinates to move to
        if (Input.GetMouseButton(0))
        {
            Vector2 newGoal = _mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
            Vector2 direction = newGoal - (Vector2)transform.position;
            _leader.SetGoal(newGoal, direction);
            _OnLeaderUpdate(newGoal, direction);
        }
    }

    public void SetOnLeaderUpdateClb(System.Action<Vector2, Vector2> clb)
    {
        _OnLeaderUpdate = clb;
    }
}


