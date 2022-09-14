using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputReceiver : MonoBehaviour
{
    private Keyboard _kb;
    [SerializeField] private Movement _movement;

    private Vector3 _movementInput;
    
    // Start is called before the first frame update
    void Start()
    {
        _kb = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        _movementInput = Vector3.zero;
        
        if (_kb.upArrowKey.isPressed)
        {
            _movementInput += Vector3.forward;
        }

        if (_kb.downArrowKey.isPressed)
        {
            _movementInput += Vector3.back;
        }

        if (_kb.leftArrowKey.isPressed)
        {
            _movementInput += Vector3.left;
        }

        if (_kb.rightArrowKey.isPressed)
        {
            _movementInput += Vector3.right;
        }
        */

        _movement.AxisInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }
}
