using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class InputListener : MonoBehaviour
{
    private Keyboard _kb;
    private Mouse _mouse;
    private Gamepad _gamepad;
    
    private Movement _movement;
    private WeaponHolder _weaponHolder;
    private CameraControls _cameraControls;
    
    void Start()
    {
        _kb = Keyboard.current;
        _mouse = Mouse.current;
        _gamepad = Gamepad.current;
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _cameraControls = Camera.main.GetComponent<CameraControls>();
    }
    
    void Update()
    {
        _movement.AxisInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _cameraControls.AxisInput(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")));
        _cameraControls.AxisInput(_gamepad.rightStick.ReadValue());
        
        
        
        // Keyboard
        
        if (_kb.leftCtrlKey.wasPressedThisFrame)
        {
            _weaponHolder.Fire();
        }

        if (_kb.qKey.wasPressedThisFrame)
        {
            _weaponHolder.NextWeapon();
        }

        if (_kb.spaceKey.wasPressedThisFrame)
        {
            _movement.Jump();
            
        }

        if (_kb.cKey.wasPressedThisFrame)
        {
            _cameraControls.ResetCamera();
        }

        // Mouse
        
        if (_mouse.leftButton.wasPressedThisFrame)
        {
            _weaponHolder.Fire();
        }

        // Gamepad

        if (_gamepad.buttonSouth.wasPressedThisFrame)
        {
            _movement.Jump();
        }

        if (_gamepad.buttonWest.wasPressedThisFrame)
        {
            _weaponHolder.Fire();
        }

        if (_gamepad.buttonNorth.wasPressedThisFrame)
        {
            _weaponHolder.NextWeapon();
        }

        if (_gamepad.rightStickButton.wasPressedThisFrame)
        {
            _cameraControls.ResetCamera();
        }
        
    }
}
