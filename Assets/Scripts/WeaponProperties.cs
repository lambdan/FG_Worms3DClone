using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Properties")]
public class WeaponProperties : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
    public float fireRate;
}
