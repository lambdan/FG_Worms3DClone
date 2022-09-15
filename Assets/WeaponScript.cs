using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _barrelExit;
    [SerializeField] private float _fireRate;
    
    private float _lastFire;
    
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
