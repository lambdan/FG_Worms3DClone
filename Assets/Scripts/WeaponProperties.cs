using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Properties")]
public class WeaponProperties : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;

    public string weaponName;
    public string weaponDescription;
    
    public float fireRate;
    

    public void Fire()
    {
        Debug.Log("Firing in Weapon Properties");
    }

    public void Reload()
    {
        Debug.Log("Reloading in Weapon Properties");
    }
}
