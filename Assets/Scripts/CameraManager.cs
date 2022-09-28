using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _timeUntilReset = 2f;
    
    private Transform _target; // Player
    private CameraGlue _cameraGlue;
    private Vector3 _cameraDestination;

    private bool _shouldFollow;
    private bool _manualControl;
    private float _manualLastTime;

    private Vector3 _manualOffset;
    private Vector2 _axisInput;
    
    void FixedUpdate()
    {
        if (_manualControl)
        {
            _cameraGlue.GetTransform().RotateAround(_target.position, new Vector3(_axisInput.y, -_axisInput.x, 0), 180 * Time.deltaTime);

            if (_cameraGlue.GetTransform().position.y < _target.position.y + 2f) // Prevent going under
            {
                _cameraGlue.GetTransform().position = new Vector3(transform.position.x, _target.position.y + 2f, transform.position.z);
            }
            
        }
        

        transform.position = _cameraGlue.GetTransform().position;
        transform.LookAt(_target);
        
        
        /*
        if (_manualControl){
            transform.RotateAround(_target.position, new Vector3(_axisInput.y, -_axisInput.x, 0), 180 * Time.deltaTime);
            _manualOffset = transform.position - _target.position;
        }
        
        if (!_manualControl && _shouldFollow) // Follow the target automatically if manual mode is not active
        {
            _cameraDestination = _target.position - (_target.transform.forward * 7) + (_target.transform.up * 4);
        }

        if (_manualControl && (Time.time - _manualLastTime) > _timeUntilReset)
        {
            // Been a while since last manual input, go back to auto
            _manualControl = false;
        }
        
        transform.position = Vector3.Lerp(transform.position, _cameraDestination, Vector3.Distance(transform.position, _cameraDestination) * Time.fixedDeltaTime);

        if (transform.position.y < _target.position.y + 2f)
        {
            transform.position = new Vector3(transform.position.x, _target.position.y + 2f, transform.position.z);
        }
        
        transform.LookAt(_target);*/

    }

    public void SetNewTarget(GameObject go, CameraGlue glue)
    {
        _target = go.transform;
        _cameraGlue = glue;
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
        /*
        transform.position = _cameraDestination;
        transform.LookAt(_target);*/
        _cameraGlue.Reset();
        _manualControl = false;
        _shouldFollow = true;
        _axisInput = Vector2.zero;
    } 
    
    public void AxisInput(Vector2 input)
    {
        _axisInput = input;
        _manualLastTime = Time.time;
        _manualControl = true;
    }
}
