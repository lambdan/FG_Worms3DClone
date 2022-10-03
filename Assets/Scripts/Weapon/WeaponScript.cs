using System;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private Transform _barrelExit;
    private float _nextFire;
    private WeaponProperties _weaponProps;
    
    private GameObject _bulletPrefab;
    private float _fireRate;

    public void SetWeaponProperties(WeaponProperties weaponProperties)
    {
        _weaponProps = weaponProperties;
    }
    
    public void Fire()
    {
        if (Time.time > _nextFire)
        {
            if (_weaponProps.RayCaster)
            {
                RaycastHit hit;
                if (Physics.Raycast(_barrelExit.position, transform.parent.forward,
                        out hit, Mathf.Infinity))
                {
                    //Debug.Log(hit.collider);
                    //Debug.DrawRay(_barrelExit.position, hit.point, Color.green, 5f);
                    DamageTaker dmgTaker = hit.collider.GetComponentInParent<DamageTaker>();
                    if (dmgTaker)
                    {
                        dmgTaker.TakeDamage(100);
                    }
                }
            }
            else
            {
                Instantiate(_weaponProps.bulletPrefab, _barrelExit.position, transform.parent.rotation);
            }
            
            _nextFire = Time.time + _fireRate;
        }
    }
}
