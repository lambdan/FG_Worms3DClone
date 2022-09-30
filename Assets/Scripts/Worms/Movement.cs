using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 7.5f;
    [SerializeField] private float _rotationSpeed = 120f;
    [SerializeField] private float _jumpForce = 2000f;
    [SerializeField] private float _fastFallTriggerVelocity = 20f;
    
    private Rigidbody _rb;
    private bool _isGrounded;
    private bool _fastFalling;
    private Vector2 _moveAxis;

    public UnityEvent landedAfterLongFall;
    

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void AxisInput(Vector2 vec)
    {
        _moveAxis = vec;
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + (transform.forward * (_moveAxis.y * Time.fixedDeltaTime * _movementSpeed)));
        Quaternion deltaRot = Quaternion.Euler(new Vector3(0, _moveAxis.x * _rotationSpeed, 0) * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * deltaRot);
        
        // PlayerInput seems to send a 0.00, 0.00 when it gets disabled which is why we don't need to think about resetting back to 0
        // ControlledByAI sends a Vector2.zero in OnDisable to achieve the same effect

        if (_rb.velocity.y < -_fastFallTriggerVelocity)
        {
            _fastFalling = true;
        }
    }

    public void MoveTowards(Vector3 pos)
    {
        RotateTowards(pos);
        _moveAxis = Vector2.up; // Simulates pressing up on a stick
    }

    public void RotateTowards(Vector3 pos)
    {
        pos = new Vector3(pos.x, transform.position.y, pos.z); // y = 0 to ignore height
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position);
        _rb.MoveRotation(targetRot);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
        }
        
    }
    
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (_fastFalling)
        {
            landedAfterLongFall.Invoke();
        }
        
        if (_rb.velocity.y <= Mathf.Abs(0.1f))
        {
            _isGrounded = true;
            _fastFalling = false;
        }
    }
}
