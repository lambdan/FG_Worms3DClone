using UnityEngine;

public abstract class PickupSO : ScriptableObject
{
    public string title;
    public string description;
    
    [Tooltip("How long the pickup is spawned until it disappears")]
    public float lifetime;

    public abstract void OnPickup(GameObject pickedUpBy);
}
