using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class CameraControls : MonoBehaviour
{
    private CameraFollow _cameraFollow;

    private Transform _target;

    private bool _active = false;

    private float _lastInput = 0;

    private Vector3 _moveVector;

    void Awake()
    {
        _cameraFollow = GetComponent<CameraFollow>(); // Get the "auto camera" system
    }

    private void LateUpdate(){
        if (_active)
        {
            transform.position += _moveVector * Time.deltaTime * 30;
            _moveVector = Vector3.zero;
            
            // Catch up to the player if we get too far away
            if (Vector3.Distance(transform.position, _target.position) > 7f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, 30*Time.deltaTime);
            }
            
            if (transform.position.y < _target.position.y) // To prevent the camera from going under the player
            {
                transform.position = new Vector3(transform.position.x, _target.position.y, transform.position.z);
            }
            
            transform.LookAt(_target);
        }
    }

    public void AxisInput(Vector2 input)
    {
        if (input.magnitude > 0.1) // Its above the "Deadzone"
        {
            _active = true; // Make manual camera movement active here
            _target = _cameraFollow.GetTarget(); // Get our player character (target) from somewhere
            _cameraFollow.Pause(); // Pause any "auto camera" system that is going
            
            // Calculate move vector (which gets used in Update()
            _moveVector = ( (transform.up * input.y) + (transform.right * input.x)).normalized;

            _lastInput = Time.time; // To keep track of when we restore to "auto camera"
        }
        else if (_active && Time.time - _lastInput > 2) // It's been a while since last user camera movement, restore to auto
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
    
    public void MakeActive()
    {
        _active = true;
    }

    public void MakeInactive()
    {
        _active = false;
    }
}
