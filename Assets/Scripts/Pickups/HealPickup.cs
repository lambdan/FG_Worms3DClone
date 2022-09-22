using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing Pickup", menuName = "Pickups/Heal", order = 0)]
public class HealPickup : PickupSO
{
    public int HealAmount;
    
    public override void OnPickup(GameObject pickedUpBy)
    {
        if (pickedUpBy.TryGetComponent(out Health health))
        {
            health.ChangeHealth(HealAmount);
        }
    }
}
