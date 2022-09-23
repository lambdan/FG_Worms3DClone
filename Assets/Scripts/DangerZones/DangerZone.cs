using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private CollisionAction _colAction;

    private void OnCollisionStay(Collision collisionInfo)
    {
        _colAction.Action(collisionInfo.gameObject);
    }
}
