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
        _bulletPrefab = weaponProperties.bulletPrefab;
        _fireRate = weaponProperties.fireRate;
    }
    
    public void Fire()
    {
        if (Time.time > _nextFire)
        {
            Instantiate(_bulletPrefab, _barrelExit.position, transform.root.rotation); // Root rotation to get direction the worm is facing
            _nextFire = Time.time + _fireRate;
        }
    }
}
