using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private float _lastFire;
    private WeaponProperties _weaponProps;

    
    private GameObject _bulletPrefab;
    private float _fireRate;
    private Transform _barrelExit;
    
    public void SetWeaponProperties(WeaponProperties WP)
    {
        _bulletPrefab = WP.bulletPrefab;
        _fireRate = WP.fireRate;
        _barrelExit = GetComponent<BarrelExit>().GetBarrelExit();
        _lastFire = 0;
    }
    
    public void Fire()
    {
        if (Time.time - _lastFire > _fireRate)
        {
            // Root rotation to get direction the worm is facing
            Instantiate(_bulletPrefab, _barrelExit.position, transform.root.rotation);
            _lastFire = Time.time;
        }
    }
}
