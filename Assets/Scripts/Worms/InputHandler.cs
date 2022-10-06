using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private GameManager _gameManager;
    private Movement _movement;
    private WeaponHolder _weaponHolder;
    private CameraManager _cameraManager;

    private bool _cameraHeld;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _cameraManager = Camera.main.GetComponent<CameraManager>();
    }
    
    public void MovementAxisInput(InputAction.CallbackContext context)
    {
        _gameManager.UpdateControllerHints(context.control.device);
        _movement.AxisInput(context.ReadValue<Vector2>());
    }

    public void CameraAxisInput(InputAction.CallbackContext context)
    {
        if (_cameraHeld)
        {
            return;
        }
        _gameManager.UpdateControllerHints(context.control.device);
        _cameraManager.AxisInput(context.ReadValue<Vector2>());
    }

    public void Jump(InputAction.CallbackContext context)
    {
        _gameManager.UpdateControllerHints(context.control.device);
        if (!context.performed)
        {
            return;
        }
        _movement.Jump();
    }

    public void NextWeapon(InputAction.CallbackContext context)
    {
        _gameManager.UpdateControllerHints(context.control.device);
        if (!context.performed)
        {
            return;
        }
        _weaponHolder.NextWeapon();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        _gameManager.UpdateControllerHints(context.control.device);
        if (!context.performed)
        {
            return;
        }

        _weaponHolder.Fire();
    }

    public void RecenterCamera(InputAction.CallbackContext context)
    {
        _gameManager.UpdateControllerHints(context.control.device);

        if (context.started)
        {
            _cameraHeld = true;
        }
        
        if (context.canceled)
        {
            _cameraHeld = false;
        }
        
        if (context.performed)
        {
            _cameraManager.InstantReset();
        }
    }

    public void ReloadWeapon(InputAction.CallbackContext context)
    {
        _gameManager.UpdateControllerHints(context.control.device);
        if (!context.performed)
        {
            return;
        }
        _weaponHolder.TriggerReload();
    }
}
