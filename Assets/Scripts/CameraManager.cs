using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _timeUntilReset = 2f;
    
    private Transform _target; // Player
    private Vector3 _cameraDestination; // Ideal position for the camera (behind the player)
    private Vector3 _moveVector; // Right stick/mouse input

    private bool _shouldFollow = false; // Auto follow the player?
    private bool _manualControl = false; // Manual control active?
    private float _manualLastTime = 0; // When was manual input last received
    
    void LateUpdate()
    {
        if (!_manualControl && _shouldFollow) // Follow the target automatically if manual mode is not active
        {
            _cameraDestination = _target.position - (_target.transform.forward * 7) + (_target.transform.up * 4);
        } else if (_manualControl && (Time.time - _manualLastTime) > _timeUntilReset)
        {
            // Been a while since last manual input, go back to auto
            _manualControl = false;
        }
        
        // Vector3 Distance to make it go faster the further away we are
        transform.position = Vector3.Lerp(transform.position, _cameraDestination, Vector3.Distance(transform.position, _cameraDestination) * Time.deltaTime);
        transform.LookAt(_target);  
    }

    public void SetNewTarget(GameObject go)
    {
        _target = go.transform;
        _shouldFollow = true;
    }
    
    public void Activate() // Used when switching a new worm
    {
        InstantReset();
    }

    public void Deactivate() // Used when the turn ends
    {
        _cameraDestination = _target.position + (transform.right*10) + (transform.forward*10) + (transform.up*10);
        _shouldFollow = false;
        _manualControl = false;
    }
    
    public void InstantReset()
    {
        transform.position = _cameraDestination;
        transform.LookAt(_target);
        _manualControl = false;
        _shouldFollow = true;
    } 
    
    public void AxisInput(Vector2 input)
    {
        if (input.magnitude > 0.1) // Its above the "Deadzone"
        {
            if (!_manualControl)
            {
                _manualControl = true;
            }

            _manualLastTime = Time.time;
            
            _moveVector = ( (transform.up * input.y) + (transform.right * input.x));
            
            _cameraDestination += _moveVector * Time.fixedDeltaTime;
            transform.LookAt(_target);
        }
    }
}
