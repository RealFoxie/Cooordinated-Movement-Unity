using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A simple Behavior to add to any object that should be destroyed when right clicked
 * Note: the object will need a collider for this to work
 */

public class DestroyOnRightClick : MonoBehaviour
{
   void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
            {
            Destroy(gameObject);
        }
    }
}
