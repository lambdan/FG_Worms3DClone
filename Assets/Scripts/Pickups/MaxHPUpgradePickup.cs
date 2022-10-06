using UnityEngine;

[CreateAssetMenu(fileName = "Max HP Upgrade Pickup", menuName = "Pickups/Max HP Up", order = 0)]

public class MaxHPUpgradePickup : PickupSO
{
    public int amount;
    
    public override void OnPickup(GameObject receiver)
    {
        if (receiver.TryGetComponent(out Health receiverHealth))
        {
            receiverHealth.ChangeMaxHealth(amount);
        }
    }
}
