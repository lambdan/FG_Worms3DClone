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

    void Start()
    {
        _kb = Keyboard.current;
        _mouse = Mouse.current;
        _gamepad = Gamepad.current;
    }

    public void SetNewTarget(InputListener target)
    {
        _IL = target;
    }

    public void DisableTarget()
    {
        _IL = null;
    }
    
    void Update()
    {
        
        // Check if pause button is pressed always, even when player isn't controlling a worm
        if (_kb.escapeKey.wasPressedThisFrame)
        {
            _GM.TogglePause();
        }
        
        if (_gamepad.startButton.wasPressedThisFrame)
        {
            _GM.TogglePause();
        }
        
        if (_IL != null)
        {
            _IL.MovementAxis(_gamepad.leftStick.ReadValue());
            _IL.CameraAxis(_gamepad.rightStick.ReadValue());
        
            // Keyboard
            if (_kb.anyKey.isPressed)
            {
                if (_kb.leftCtrlKey.wasPressedThisFrame)
                {
                    _IL.Fire();
                }

                if (_kb.qKey.wasPressedThisFrame)
                {
                    _IL.NextWeapon();
                }

                if (_kb.eKey.wasPressedThisFrame)
                {
                    _WM.NextWorm();
                }
        
                if (_kb.spaceKey.wasPressedThisFrame)
                {
                    _IL.Jump();
                }
        
                if (_kb.cKey.wasPressedThisFrame)
                {
                    _IL.Recenter();
                }


            }

            // Mouse
            if (_mouse.leftButton.wasPressedThisFrame)
            {
                _IL.Fire();
            }

            // Gamepad
            if (_gamepad.wasUpdatedThisFrame)
            {
                if (_gamepad.buttonSouth.wasPressedThisFrame)
                {
                    _IL.Jump();
                }

                if (_gamepad.buttonWest.wasPressedThisFrame)
                {
                    _IL.Fire();
                }

                if (_gamepad.buttonNorth.wasPressedThisFrame)
                {
                    _IL.NextWeapon();
                }

                if (_gamepad.rightStickButton.wasPressedThisFrame)
                {
                    _IL.Recenter();
                }

                if (_gamepad.rightShoulder.wasPressedThisFrame)
                {
                    _WM.NextWorm(); 
                }
            }
        }
    }
}
