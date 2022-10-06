using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Pickup", menuName = "Pickups/Weapon", order = 0)]
public class WeaponPickup : PickupSO
{
    public WeaponProperties _weaponProperty;
    public int ammo;
    
    public override void OnPickup(GameObject receiver)
    {
        if (receiver.TryGetComponent(out WeaponHolder receiverWeaponHolder))
        {
            receiverWeaponHolder.GetNewWeapon(_weaponProperty, ammo);
        }
    }

}
