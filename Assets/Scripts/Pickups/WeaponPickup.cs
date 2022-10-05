using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Pickup", menuName = "Pickups/Weapon", order = 0)]
public class WeaponPickup : PickupSO
{
    public WeaponProperties _weaponProperty;
    public int ammo;
    
    public override void OnPickup(GameObject pickedUpBy)
    {
        if (pickedUpBy.TryGetComponent(out WeaponHolder wh))
        {
            wh.GetNewWeapon(_weaponProperty, ammo);
        }
    }

}
