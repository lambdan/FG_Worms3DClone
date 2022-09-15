using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private CollisionAction _colAction;
    [SerializeField] private bool _destroyOnCollision;
    
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision detected in CollisionDetector");
        _colAction.Action(collision.gameObject);

        if (_destroyOnCollision)
        {
            Destroy(this); // Destroy bullet on collision
        }
    }
}
