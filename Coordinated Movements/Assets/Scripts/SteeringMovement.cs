using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Movement logic to move a unit programatically with steering.
* Basic straight steering is implemented here.
*/
public class SteeringMovement : MonoBehaviour
{
    Vector2 _goal;
    // can the unit move and is it not yet at its goal
    bool _isMoving;
    [SerializeField] float _speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.Equals(_goal))
        {
            _isMoving = false;
        }
        if(_isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _goal, _speed * Time.deltaTime);
        }
    }

    public void SetGoal(Vector2 goal)
    {
        _goal = goal;
        _isMoving = true;
    }
}
