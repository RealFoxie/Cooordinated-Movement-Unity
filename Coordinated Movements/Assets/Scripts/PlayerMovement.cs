using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    public float _speed = 10f;
    Vector2 _lastClickedPos;
    bool _moving;

    private void Update()
    {
        // New coordinates to move to
        if (Input.GetMouseButton(0))
        {
            _lastClickedPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _moving = true;
        }

        // Arrived at destination
        if (transform.position.Equals(_lastClickedPos))
        {
            _moving = false;
        }

        // Move closer if active moving
        if (_moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _lastClickedPos, _speed * Time.deltaTime);
        }
    }
}


