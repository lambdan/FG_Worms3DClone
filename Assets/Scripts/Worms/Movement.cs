using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpForce;

    private bool _isGrounded = true;
    private bool _falling = false;

    public UnityEvent landedAfterLongFall;

    private Vector2 _moveAxis;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void AxisInput(Vector2 move)
    {
        if (move.magnitude > 0.1)
        {
            _moveAxis = move;
        }
        
    }

    void FixedUpdate()
    {
        if (_moveAxis != Vector2.zero)
        {
            _rb.MovePosition(_rb.position + (transform.forward * (_moveAxis.y * Time.fixedDeltaTime * movementSpeed)));
        
            Quaternion deltaRot = Quaternion.Euler(new Vector3(0,_moveAxis.x*rotationSpeed,0) * Time.fixedDeltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRot);
        
            _moveAxis = Vector2.zero;  
        }

        if (_rb.velocity.y < -20)
        {
            _falling = true;
        }
    }

    public void MoveTowards(Vector3 pos)
    {
        RotateTowards(pos);
        _moveAxis = Vector2.up; // Simulates pressing up on a stick
    }

    public void RotateTowards(Vector3 pos)
    {
        pos = new Vector3(pos.x, 0, pos.z); // y = 0 to ignore height
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position);
        _rb.MoveRotation(targetRot);
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
        if (_falling)
        {
            landedAfterLongFall.Invoke();
        }
        
        if (_rb.velocity.y <= Mathf.Abs(0.1f))
        {
            _isGrounded = true;
            _falling = false;
        }
    }
}
