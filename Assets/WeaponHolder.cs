using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform _weaponHand;
    [SerializeField] private GameObject _weaponPrefab;

    private GameObject _currentWeapon;
    private WeaponScript _currentWeaponScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    
    

    public void SwitchWeapon(GameObject prefab)
    {
        Debug.Log("Switch weapon");
        if (_currentWeapon != null)
        {
            Destroy(_currentWeapon); // Remove current weapon
        }
        
        GameObject weapon = Instantiate(_weaponPrefab, _weaponHand.position, _weaponHand.rotation, _weaponHand); 
        _currentWeapon = weapon;
        _currentWeaponScript = weapon.GetComponent<WeaponScript>();

    }

    public void NextWeapon()
    {
        // TODO go through a list of weapon prefabs here
        SwitchWeapon(_weaponPrefab);
    }
    
    public void Fire()
    {
        Debug.Log("Firing in weapon holder!!!");
        _currentWeaponScript.Fire();
    }
}
