using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class InputListener : MonoBehaviour
{
    private Keyboard _kb;
    private Mouse _mouse;
    
    private Movement _movement;
    private WeaponHolder _weaponHolder;
    private CameraControls _cameraControls;
    
    // Start is called before the first frame update
    void Start()
    {
        _kb = Keyboard.current;
        _mouse = Mouse.current;
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _cameraControls = Camera.main.GetComponent<CameraControls>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement.AxisInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _cameraControls.AxisInput(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

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

        // Mouse
        
        if (_mouse.leftButton.wasPressedThisFrame)
        {
            _weaponHolder.Fire();
        }
        
    }
}
