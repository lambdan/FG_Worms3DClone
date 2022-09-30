using UnityEngine;
public class CameraManager : MonoBehaviour
{
    public float cameraOverviewSpeed = 0.6f;
    private Transform _target; // Player
    private CameraGlue _cameraGlue;
    //private bool _shouldFollow;
    private bool _manualControl;
    private bool _overviewMode;
    private Vector3 _manualOffset;
    private Vector2 _axisInput;
    private Vector3 _cameraDestination;
    
    void FixedUpdate()
    {
        if (_manualControl)
        {
            _cameraGlue.GetTransform().RotateAround(_target.position, new Vector3(-_axisInput.y, -_axisInput.x, 0), 180 * Time.deltaTime);
            if (_cameraGlue.GetTransform().position.y < _target.position.y + 2f) // Prevent going under
            {
                _cameraGlue.GetTransform().position = new Vector3(_cameraGlue.GetTransform().position.x, _target.position.y + 2f, _cameraGlue.GetTransform().position.z);
            }
        } 
        
        if (_overviewMode)
        {
            _cameraDestination = transform.position + (cameraOverviewSpeed * Vector3.up);
        } 
        else
        {
            _cameraDestination = _cameraGlue.GetTransform().position;
        }

        transform.position = _cameraDestination;
        transform.LookAt(_target);
    }

    public void SetNewTarget(GameObject go, CameraGlue glue)
    {
        _target = go.transform;
        _cameraGlue = glue;
        _overviewMode = false;
        //_shouldFollow = true;
    }
    
    public void Activate() // Used when switching a new worm
    {
        InstantReset();
    }

    public void Deactivate() // Used when the turn ends
    {
        _manualControl = false;
        _overviewMode = true;
    }
    
    public void InstantReset()
    {
        _cameraGlue.Reset();
        _manualControl = false;
        //_shouldFollow = true;
        _axisInput = Vector2.zero;
    } 
    
    public void AxisInput(Vector2 input)
    {
        _axisInput = input;
        _manualControl = true;
    }
}
