using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReceiver : MonoBehaviour
{
    private Keyboard _kb;
    
    private Movement _movement;
    
    // Start is called before the first frame update
    void Start()
    {
        _kb = Keyboard.current;
        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement.AxisInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
