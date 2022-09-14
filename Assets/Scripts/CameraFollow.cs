using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _cameraGlue;
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed;
    
    private Vector3 _lastPlayerPos;
    
    void LateUpdate()
    {
        if (_lastPlayerPos != _target.position) // Player moving, recenter camera
        {
            transform.position = _cameraGlue.position; // Instantly move camera to its ideal ("glue") position
            
            // Rotate it so it looks at the player
            Quaternion q = Quaternion.LookRotation(_target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, _rotationSpeed*Time.deltaTime);
            
            // Store last player position so we can keep track if player moved
            _lastPlayerPos = _target.position;
        }
    }
}
