using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class InputReceiver : MonoBehaviour
{
    private Keyboard _kb;
    
    private Movement _movement;
    private WeaponHolder _weaponHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        _kb = Keyboard.current;
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement.AxisInput(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_kb.spaceKey.wasPressedThisFrame)
        {
            _weaponHolder.Fire();
        }

        if (_kb.qKey.wasPressedThisFrame)
        {
            _weaponHolder.NextWeapon();
        }
        
    }
}
