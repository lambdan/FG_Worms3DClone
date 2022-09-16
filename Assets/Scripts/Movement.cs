using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    
    public void AxisInput(float horizontal, float vertical)
    {
        Vector3 rotation = new Vector3(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized * Time.deltaTime;
        transform.Translate(move * movementSpeed);
        transform.Rotate(rotation);
    }

    public void MoveTowards(Vector3 pos)
    {
        RotateTowards(pos);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    public void RotateTowards(Vector3 pos)
    {
        pos = new Vector3(pos.x, 0, pos.z);
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.fixedDeltaTime * 2);
    }

}
