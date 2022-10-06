using UnityEngine;

[CreateAssetMenu(fileName = "Healing Pickup", menuName = "Pickups/Heal", order = 0)]
public class HealPickup : PickupSO
{
    public int HealAmount;
    
    public override void OnPickup(GameObject receiver)
    {
        if (receiver.TryGetComponent(out Health receiverHealth))
        {
            receiverHealth.ChangeHealth(HealAmount);
        }
    }
}
