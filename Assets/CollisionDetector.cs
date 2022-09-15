using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private CollisionAction _colAction;
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision detected in CollisionDetector");
        _colAction.Action(collision.gameObject);
    }
}
