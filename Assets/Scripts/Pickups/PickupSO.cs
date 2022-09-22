using UnityEngine;

public abstract class PickupSO : ScriptableObject
{
    public string title;
    public string description;

    public abstract void OnPickup(GameObject pickedUpBy);
}
