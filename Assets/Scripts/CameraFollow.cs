using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CameraControls _cameraControls;
    private Transform _target;
    private bool _active = false;
    private bool _paused = false;

    private Vector3 _cameraDestination;

    void LateUpdate()
    {
        if (_paused)
        {
            return;
        }
        
        if (_active && _target  != null)
        {
            _cameraDestination = _target.position - (_target.transform.forward * 5) + (_target.transform.up * 3);
        }
        
        transform.position = Vector3.Lerp(transform.position, _cameraDestination, Vector3.Distance(transform.position, _cameraDestination) * Time.deltaTime);
        Quaternion rot = Quaternion.LookRotation(_target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);
    }


    private void Awake()
    {
        _cameraControls = GetComponent<CameraControls>();
    }

    public void SetNewTarget(GameObject go)
    {
        _target = go.transform;
    }
    
    public void Activate()
    {
        _cameraControls.MakeInactive(); // Deactivate the manual camera
        _active = true;
        _paused = false;
    }

    public void Deactivate()
    {
        _active = false;
        _cameraDestination = _target.position + (transform.right*10) + (transform.forward*10) + (transform.up*10);
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    }
    
    public bool GetState()
    {
        return _active;
    }

    public Transform GetTarget()
    {
        return _target;
    }


}
