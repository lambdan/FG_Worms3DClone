using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bad Pickup", menuName = "Pickups/Bad", order = 0)]
public class BadPickup : PickupSO
{
    public int maxHPdecrease;
    public int damageAmount;
    
    public override void OnPickup(GameObject pickedUpBy)
    {
        if (pickedUpBy.TryGetComponent(out Health health))
        {
            health.ChangeMaxHealth(-maxHPdecrease);
            health.ChangeHealth(-damageAmount);
        }
    }
}
