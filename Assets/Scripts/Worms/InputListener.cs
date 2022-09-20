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
    private WormManager _wormManager;

    void Awake()
    {
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _cameraControls = Camera.main.GetComponent<CameraControls>();
    }
    public void MovementAxis(Vector2 axises)
    {
        _movement.AxisInput(axises);
    }

    public void CameraAxis(Vector2 axises)
    {
        _cameraControls.AxisInput(axises);
    }

    public void Fire()
    {
        _weaponHolder.Fire();
    }

    public void NextWeapon()
    {
        _weaponHolder.NextWeapon();
    }

    public void Jump()
    {
        _movement.Jump();
    }

    public void Recenter()
    {
        _cameraControls.ResetCamera();
    }
    
}
