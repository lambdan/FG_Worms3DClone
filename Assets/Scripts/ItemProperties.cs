using UnityEngine;

[CreateAssetMenu(menuName = "Item Properties")]
public class ItemProperties : ScriptableObject
{
    public int healAmount;
    public int maxHPIncrease;
    public int invincibilityTime;
}