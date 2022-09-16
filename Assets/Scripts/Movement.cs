using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    
    //private CharacterController _characterController;
    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        //_characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AxisInput(float horizontal, float vertical)
    {
        Debug.Log(horizontal + "," + vertical);
        Vector3 rotation = new Vector3(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized * Time.deltaTime;
        transform.Translate(move * movementSpeed);
        transform.Rotate(rotation);
    }

    public void MoveTowards(Vector3 pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * movementSpeed);
        RotateTowards(pos);
    }

    public void RotateTowards(Vector3 pos)
    {
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.fixedDeltaTime * 2);
        //transform.LookAt(pos);
    }

}
