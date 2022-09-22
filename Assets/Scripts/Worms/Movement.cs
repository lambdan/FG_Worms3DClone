using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpForce;

    private bool _isGrounded = true;

    private Vector3 _moveAxis;

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
        _rb.MovePosition(_rb.position + (transform.forward * (_moveAxis.y * Time.fixedDeltaTime * movementSpeed)));
        
        Quaternion deltaRot = Quaternion.Euler(new Vector3(0,_moveAxis.x*rotationSpeed,0) * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * deltaRot);
        
        _moveAxis = Vector2.zero;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        
    }

    /*
    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Walls"))
        {
            Debug.Log("nope");
            _rb.velocity = Vector3.zero;
        }
    }*/

    private void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log(collisionInfo.gameObject.name + "," + collisionInfo.gameObject.tag);
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}
