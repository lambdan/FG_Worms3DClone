using UnityEngine;

[CreateAssetMenu(fileName = "Max HP Upgrade Pickup", menuName = "Pickups/Max HP Up", order = 0)]

public class MaxHPUpgradePickup : PickupSO
{
    public int amount;
    
    public override void OnPickup(GameObject pickedUpBy)
    {
        if (pickedUpBy.TryGetComponent(out Health health))
        {
            health.ChangeMaxHealth(amount);
        }
    }
}
