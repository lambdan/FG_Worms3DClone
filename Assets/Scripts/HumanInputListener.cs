using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(WormManager))]
public class HumanInputListener : MonoBehaviour
{
    private Keyboard _kb;
    private Mouse _mouse;
    private Gamepad _gamepad;

    private GameManager _GM;
    private WormManager _WM;
    private InputListener _IL;
    
    void Awake()
    {
        _GM = GetComponent<GameManager>();
        _WM = GetComponent<WormManager>();
    }
    public void SetNewTarget(InputListener target)
    {
        _IL = target;
    }

    public void DisableTarget()
    {
        _IL = null;
    }

    void CheckPause()
    {
        if (_kb.escapeKey.wasPressedThisFrame || (_gamepad != null && _gamepad.startButton.wasPressedThisFrame))
        {
            _GM.TogglePause();
        }
    }

    void CheckFire()
    {
        if (_kb.leftCtrlKey.wasPressedThisFrame || _mouse.leftButton.wasPressedThisFrame || (_gamepad != null && _gamepad.buttonWest.wasPressedThisFrame))
        {
            _IL.Fire();
        }
    }

    void CheckNextWeapon()
    {
        if (_kb.qKey.wasPressedThisFrame || (_gamepad != null && _gamepad.buttonNorth.wasPressedThisFrame))
        {
            _IL.NextWeapon();
        }
    }

    void CheckReload()
    {
        if (_kb.rKey.wasPressedThisFrame || (_gamepad != null && _gamepad.buttonEast.wasPressedThisFrame))
        {
            _IL.ReloadWeapon();
        }
    }

    void CheckRecenter()
    {
        if (_kb.cKey.wasPressedThisFrame || (_gamepad != null && _gamepad.rightStickButton.wasPressedThisFrame))
        {
            _IL.Recenter();
        } 
    }

    void CheckJump()
    {
        if (_kb.spaceKey.wasPressedThisFrame || (_gamepad != null && _gamepad.buttonSouth.wasPressedThisFrame))
        {
            _IL.Jump();
        } 
    }

    void CheckNextWorm()
    {
        if (_kb.eKey.wasPressedThisFrame || (_gamepad != null && _gamepad.rightShoulder.wasPressedThisFrame))
        {
            _WM.NextWorm();
        } 
    }

    void Update()
    {
        _kb = Keyboard.current;
        _mouse = Mouse.current;
        _gamepad = Gamepad.current;
        
        CheckPause(); // Always check if pause button is pressed

        if (_IL != null) // Only go through worm inputs if we have a human controlled worm active
        {
            _IL.MovementAxis(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))); // Handles both wasd/arrow keys and left stick on gamepad
            
            _IL.CameraAxis(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 10); // Camera movement through mouse
            
            if (_gamepad != null) // Camera movement through gamepad
            {
                _IL.CameraAxis(_gamepad.rightStick.ReadValue());
            }
            
            CheckFire();
            CheckReload();
            CheckNextWeapon();
            CheckJump();

            CheckNextWorm();
            CheckRecenter();
        }
    }
}
