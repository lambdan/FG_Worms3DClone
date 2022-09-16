using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    
    private CharacterController _characterController;
    //private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //_rigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveForward()
    {
        _characterController.Move(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    public void MoveBackwards()
    {
        _characterController.Move(Vector3.back * movementSpeed * Time.deltaTime);
    }

    public void StrafeLeft()
    {
        //_rigidbody.freezeRotation = true; // Hack to fix bug where worm spins when strafing sometimes
        _characterController.Move(Vector3.left * movementSpeed * Time.deltaTime);
    }

    public void StrafeRight()
    {
        //_rigidbody.freezeRotation = true;
        _characterController.Move(Vector3.right * movementSpeed * Time.deltaTime);
    }

    public void AxisInput(float horizontal, float vertical)
    {
        Vector3 rotation = new Vector3(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        Vector3 move = new Vector3(0, 0, vertical * Time.deltaTime);
        move = transform.TransformDirection(move);
        _characterController.Move(move * movementSpeed);
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
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.fixedDeltaTime * 5);
    }

}
