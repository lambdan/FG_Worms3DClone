using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpForce;

    private bool _isGrounded = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    

    public void AxisInput(float horizontal, float vertical)
    {
        /*
        Vector3 rotation = new Vector3(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;
        transform.Translate(move * (movementSpeed * Time.deltaTime));
        transform.Rotate(rotation);*/
        //Vector3 move = ((horizontal * Vector3.right) + (vertical * Vector3.forward)).normalized;
        transform.Translate(Vector3.forward * vertical * movementSpeed * Time.deltaTime);
        transform.Rotate(0, horizontal * 180f * Time.deltaTime, 0); // Rotate character towards direction stick is pressed
    }

    public void MoveTowards(Vector3 pos)
    {
        RotateTowards(pos);
        transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
    }

    public void RotateTowards(Vector3 pos)
    {
        pos = new Vector3(pos.x, 0, pos.z);
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.fixedDeltaTime * 2);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("World"))
        {
            _isGrounded = true;
        }
    }
}
