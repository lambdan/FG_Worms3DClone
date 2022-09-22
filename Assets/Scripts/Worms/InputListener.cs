using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class InputListener : MonoBehaviour
{
    private Movement _movement;
    private WeaponHolder _weaponHolder;
    private CameraManager _cameraMan;

    void Awake()
    {
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _cameraMan = Camera.main.GetComponent<CameraManager>();
    }

    public void MovementAxis(Vector2 axises)
    {
        _movement.AxisInput(axises);
    }

    public void CameraAxis(Vector2 axises)
    {
        _cameraMan.AxisInput(axises);
    }

    public void Fire()
    {
        _weaponHolder.Fire();
    }

    public void NextWeapon()
    {
        _weaponHolder.NextWeapon();
    }

    public void Jump()
    {
        _movement.Jump();
    }

    public void Recenter()
    {
        _cameraMan.InstantReset();
    }
    
}
