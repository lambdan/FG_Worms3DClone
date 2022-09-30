using UnityEngine;

public class CameraGlue : MonoBehaviour
{
    [SerializeField] private Transform _cameraGlueTransform;

    private Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = _cameraGlueTransform.localPosition;
    }
    
    public Transform GetTransform()
    {
        return _cameraGlueTransform;
    }

    public void Reset()
    {
        _cameraGlueTransform.localPosition = _initialPosition;
    }
}
