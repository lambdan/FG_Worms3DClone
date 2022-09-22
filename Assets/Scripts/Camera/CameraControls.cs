using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraControls : MonoBehaviour
{
    private CameraFollow _cameraFollow;
    private Transform _target;
    private bool _active = false;
    private Vector3 _moveVector;

    void Awake()
    {
        _cameraFollow = GetComponent<CameraFollow>(); // Get the "auto camera" system
    }

    private void LateUpdate(){
        if (_active)
        {
            if (transform.position.y < _target.position.y + 1f) // To prevent the camera from going under the player
            {
                transform.position = new Vector3(transform.position.x, _target.position.y + 1f, transform.position.z);
            }
        }
    }

    public void AxisInput(Vector2 input)
    {
        if (input.magnitude > 0.1) // Its above the "Deadzone"
        {
            if (!_active)
            {
                _active = true;
            }
            
            _target = _cameraFollow.GetTarget(); // Get our player character (target) from somewhere
            _cameraFollow.Pause(); // Pause any "auto camera" system that is going

            _moveVector = ( (transform.up * input.y) + (transform.right * input.x));
            
            transform.position += _moveVector * Time.fixedDeltaTime;
            transform.LookAt(_target);
        }
        else if (_active) // It's been a while since last user camera movement, restore to auto
        {
            _active = false;
            _cameraFollow.Unpause();
        }
    }

    public void ResetCamera()
    {
        _active = false;
        _cameraFollow.Unpause();
        _cameraFollow.InstantReset();
    }
    
    public void MakeInactive()
    {
        _active = false;
    }
}
