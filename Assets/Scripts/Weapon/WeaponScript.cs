using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private float _nextFire = 0;
    private WeaponProperties _weaponProps;
    
    private GameObject _bulletPrefab;
    private float _fireRate;
    private Transform _barrelExit;
    
    public void SetWeaponProperties(WeaponProperties WP)
    {
        _bulletPrefab = WP.bulletPrefab;
        _fireRate = WP.fireRate;
        _barrelExit = GetComponent<BarrelExit>().GetBarrelExit();
    }
    
    public void Fire()
    {
        if (Time.time > _nextFire)
        {
            // Root rotation to get direction the worm is facing
            Instantiate(_bulletPrefab, _barrelExit.position, transform.root.rotation);
            _nextFire = Time.time + _fireRate;
        }
    }
}
