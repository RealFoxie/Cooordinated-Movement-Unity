using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Math;
/*
* Movement logic to move an agent programatically with steering.
* Basic straight steering with basic rotation is implemented here.
*/
public class SteeringMovement : MonoBehaviour
{
    Vector2 _goal;
    public Vector2 Goal
    {
        get { return _goal; }
    }
    Vector2 _goalDirection;
    public Vector2 Direction
    {
        get { return _goalDirection; }
    }
    // can the unit move and is it not yet at its goal
    bool _isMoving;
    [SerializeField] float _speed = 10f;
    //rotations per second
    [SerializeField] float _speedRotation = 0.2f;

    System.Action<Vector2, Vector2> _OnGoalChangeClb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.Equals(_goal) && transform.up.Equals(_goalDirection))
        {
            _isMoving = false;
        }
        if(_isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _goal, _speed * Time.deltaTime);
            float rotationAmount = _speedRotation * (Mathf.PI + Mathf.PI);
            Vector2 direction = Vector3.RotateTowards(transform.up, _goalDirection, rotationAmount * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(transform.forward, direction);
        }
    }

    public void SetGoal(Vector2 goal, Vector2 goalDirection)
    {
        _goal = goal;
        Vector2 newDirection = goalDirection.normalized;
        // keep original direction if new one is 0, as there is no valid new direction then.
        if (!newDirection.SqrMagnitude().Equals(0))
        {
            _goalDirection = newDirection;
        }
        _isMoving = true;
        if(_OnGoalChangeClb != null)
        {
            _OnGoalChangeClb(goal, goalDirection);
        }
    }

    public void SetOnGoalChangeClb(System.Action<Vector2, Vector2> clb)
    {
        _OnGoalChangeClb = clb;
    }
}
