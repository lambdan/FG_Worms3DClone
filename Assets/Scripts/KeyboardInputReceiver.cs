using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputReceiver : MonoBehaviour
{
    private Keyboard _kb;
    [SerializeField] private Movement _movement;
    
    // Start is called before the first frame update
    void Start()
    {
        _kb = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (_kb.upArrowKey.isPressed)
        {
            _movement.MoveForward();
        }

        if (_kb.downArrowKey.isPressed)
        {
            _movement.MoveBackwards();
        }

        if (_kb.leftArrowKey.isPressed)
        {
            _movement.StrafeLeft();
        }

        if (_kb.rightArrowKey.isPressed)
        {
            _movement.StrafeRight();
        }
    }
}
