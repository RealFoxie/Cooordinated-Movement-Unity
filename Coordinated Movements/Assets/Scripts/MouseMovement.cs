using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    public float _speed = 10f;
    Vector2 _lastClickedPos;
    bool _Ismoving;
    System.Action<Vector2> _OnLeaderUpdate;

    private void Update()
    {
        // New coordinates to move to
        if (Input.GetMouseButton(0))
        {
            _lastClickedPos = _mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
            _Ismoving = true;
            _OnLeaderUpdate(_lastClickedPos);
        }

        // Arrived at destination
        if (transform.position.Equals(_lastClickedPos))
        {
            _Ismoving = false;
        }

        // Move closer if active moving
        if (_Ismoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _lastClickedPos, _speed * Time.deltaTime);
        }
    }

    public void SetOnLeaderUpdateClb(System.Action<Vector2> clb)
    {
        _OnLeaderUpdate = clb;
    }
}


