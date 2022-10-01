using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Movement _movement;
    private WeaponHolder _weaponHolder;
    private CameraManager _cameraManager;

    void Awake()
    {
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _cameraManager = Camera.main.GetComponent<CameraManager>();
    }
    
    public void MovementAxisInput(InputAction.CallbackContext context)
    {
        _movement.AxisInput(context.ReadValue<Vector2>());
    }

    public void CameraAxisInput(InputAction.CallbackContext context)
    {
        _cameraManager.AxisInput(context.ReadValue<Vector2>());
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        _movement.Jump();
    }

    public void NextWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        _weaponHolder.NextWeapon();
    }

    public void Attack(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            _weaponHolder.Fire();
        }

    public void RecenterCamera(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        _cameraManager.InstantReset();
    }

    public void ReloadWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        _weaponHolder.TriggerReload();
    }
}
