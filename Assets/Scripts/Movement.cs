using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
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
    
}
