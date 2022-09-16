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
    
    void Update()
    {
        _movement.AxisInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }
}
