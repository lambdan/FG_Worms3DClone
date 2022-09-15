using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _cameraGlue;
    [SerializeField] private Transform _target;

    private Vector3 _lastPlayerPos;
    
    void LateUpdate()
    {
        if (_lastPlayerPos != _target.position) // Player moving, recenter camera
        {
            // Instantly move camera to its ideal ("glue") position
            transform.position = _cameraGlue.position; 
            
            // Rotate camera so it looks at the player
            transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
            
            // Store last player position so we can keep track if player moved
            _lastPlayerPos = _target.position;
        }
    }

    public void SetNewTarget(GameObject go)
    {
        _cameraGlue = go.GetComponent<WormInfo>().GetCameraGlue().transform;
        _target = go.transform;
        
        // Instantly move camera
        transform.position = _cameraGlue.position;
        transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
    }
}
