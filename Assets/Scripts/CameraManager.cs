using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _timeUntilReset = 2f;
    
    private Transform _target; // Player
    private Vector3 _cameraDestination;

    private bool _shouldFollow = false; // Auto follow the player?
    private bool _manualControl = false; // Manual control active?
    private float _manualLastTime = 0; // When was manual input last received

    private Vector3 _manualOffset;
    
    void LateUpdate()
    {
        if (!_manualControl && _shouldFollow) // Follow the target automatically if manual mode is not active
        {
            _cameraDestination = _target.position - (_target.transform.forward * 7) + (_target.transform.up * 4);
        }
        else
        {
            _cameraDestination = _target.position + _manualOffset;
        }

        if (_manualControl && (Time.time - _manualLastTime) > _timeUntilReset)
        {
            // Been a while since last manual input, go back to auto
            _manualControl = false;
        }
        
        transform.position = Vector3.Lerp(transform.position, _cameraDestination, Vector3.Distance(transform.position, _cameraDestination) * Time.deltaTime);

        if (transform.position.y < _target.position.y + 2f)
        {
            transform.position = new Vector3(transform.position.x, _target.position.y + 2f, transform.position.z);
        }
        
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
            
            transform.RotateAround(_target.position, new Vector3(input.y, -input.x, 0), 180 * Time.deltaTime);
            _manualOffset = transform.position - _target.position;
        }
    }
}
