using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    private CharacterController _characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
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
        _characterController.Move(Vector3.left * movementSpeed * Time.deltaTime);
    }

    public void StrafeRight()
    {
        _characterController.Move(Vector3.right * movementSpeed * Time.deltaTime);
    }
    
}
