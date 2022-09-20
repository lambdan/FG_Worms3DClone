using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputs : MonoBehaviour
{
    private Keyboard _kb;
    private Gamepad _gp;
    [SerializeField] private MenuManager _mm;
    
    // Start is called before the first frame update
    void Start()
    {
        _kb = Keyboard.current;
        _gp = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (_kb.anyKey.wasPressedThisFrame)
        {
            if (_kb.enterKey.wasPressedThisFrame)
            {
                _mm.Select();
            }
            
            if (_kb.upArrowKey.wasPressedThisFrame)
            {
                _mm.MoveUp();
            }

            if (_kb.downArrowKey.wasPressedThisFrame)
            {
                _mm.MoveDown();
            }
        }

        if (_gp.wasUpdatedThisFrame)
        {
            if (_gp.buttonSouth.wasPressedThisFrame)
            {
                _mm.Select();
            }

            if (_gp.dpad.down.wasPressedThisFrame)
            {
                _mm.MoveDown();
            }

            if (_gp.dpad.up.wasPressedThisFrame)
            {
                _mm.MoveUp();
            }
            
        }
    }
}
