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
    

    public void AxisInput(Vector2 move)
    {
        transform.Translate(Vector3.forward * (move.y * movementSpeed * Time.deltaTime));
        transform.Rotate(0, move.x * rotationSpeed * Time.deltaTime, 0); // Rotate character towards direction stick is pressed
    }

    public void MoveTowards(Vector3 pos)
    {
        RotateTowards(pos);
        transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
    }

    public void RotateTowards(Vector3 pos)
    {
        pos = new Vector3(pos.x, 0, pos.z); // y = 0 to ignore height
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.fixedDeltaTime);
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
