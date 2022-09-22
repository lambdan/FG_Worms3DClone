using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class InputListener : MonoBehaviour
{
    private Movement _movement;
    private WeaponHolder _weaponHolder;
    private CameraControls _cameraControls;

    void Awake()
    {
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _cameraControls = Camera.main.GetComponent<CameraControls>();
    }

    public void MovementAxis(Vector2 axises)
    {
        _movement.AxisInput(axises);
    }

    public void CameraAxis(Vector2 axises)
    {
        _cameraControls.AxisInput(axises);
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
        _cameraControls.ResetCamera();
    }
    
}
