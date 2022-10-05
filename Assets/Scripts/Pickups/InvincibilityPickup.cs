using UnityEngine;

[CreateAssetMenu(fileName = "Invincibility Pickup", menuName = "Pickups/Invincibility", order = 0)]

public class InvincibilityPickup : PickupSO
{
    public float duration;
    
    public override void OnPickup(GameObject pickedUpBy)
    {
        if (pickedUpBy.TryGetComponent(out Health health))
        {
            health.StartInvincibility(duration);
        }
    }
}
