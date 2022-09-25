using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private Transform _barrelExit;
    private float _nextFire = 0;
    private WeaponProperties _weaponProps;
    
    private GameObject _bulletPrefab;
    private float _fireRate;

    public void SetWeaponProperties(WeaponProperties WP)
    {
        _bulletPrefab = WP.bulletPrefab;
        _fireRate = WP.fireRate;
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
