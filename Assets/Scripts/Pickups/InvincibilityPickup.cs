using UnityEngine;

[CreateAssetMenu(fileName = "Invincibility Pickup", menuName = "Pickups/Invincibility", order = 0)]

public class InvincibilityPickup : PickupSO
{
    public float duration;
    
    public override void OnPickup(GameObject receiver)
    {
        if (receiver.TryGetComponent(out Health receiverHealth))
        {
            receiverHealth.StartInvincibility(duration);
        }
    }
}
