using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private List<WeaponProperties> allWeapons;
    [SerializeField] private bool _testGiveWeapon;
    [SerializeField] private WeaponProperties _testGive;
    [SerializeField] private Transform _weaponHand;
    [SerializeField] private Slider _cooldownSlider;

    private List<WeaponProperties> _heldWeapons = new List<WeaponProperties>();
    private int _currentWeaponIndex = 0;
    private WeaponProperties _currentWeaponProperties;
    private GameObject _currentWeaponObject;
    private WeaponScript _currentWeaponScript;

    private float _nextFire = 0;

    void Awake()
    {
        _cooldownSlider.gameObject.SetActive(false); // Hide cooldown slider
    }

    void Start()
    {
        foreach (WeaponProperties WP in allWeapons)
        {
            GetNewWeapon(WP);
        }
    }


    public void SwitchWeapon(WeaponProperties WepProps)
    {
        if (_currentWeaponObject != null)
        {
            Destroy(_currentWeaponObject); // Remove current weapon
        }

        _currentWeaponProperties = WepProps;
        
        GameObject weapon = Instantiate(_currentWeaponProperties.weaponPrefab, _weaponHand.position,
            _weaponHand.rotation, _weaponHand);
        _currentWeaponObject = weapon;
        _currentWeaponScript = _currentWeaponObject.GetComponent<WeaponScript>();
        _currentWeaponScript.SetWeaponProperties(_currentWeaponProperties);
    }

    public void GetNewWeapon(WeaponProperties WepProps)
    {
        // Check that we don't already have this weapon
        foreach (WeaponProperties WP in _heldWeapons)
        {
            if (WP.name == WepProps.name)
            {
                Debug.Log("We already have this weapon... not picking it up again");
                return;
            }
        }
        
        _heldWeapons.Add(WepProps);
        
        // Switch to latest picked up weapon (its gonna be at the end of the list)
        SwitchWeapon(_heldWeapons[_heldWeapons.Count - 1]);
    }
    public void NextWeapon()
    {
        if (_heldWeapons.Count == 1)
        {
            // Only have 1 gun, nothing to switch to
            return;
        }
        
        int next = _currentWeaponIndex + 1;
        if (next >= _heldWeapons.Count)
        {
            next = 0; // Back to first weapon
        }

        _currentWeaponIndex = next;
        SwitchWeapon(_heldWeapons[next]);

    }
    
    public void Fire()
    {
        if (Time.time > _nextFire)
        {
            _currentWeaponScript.Fire();
            
            _nextFire = Time.time + _currentWeaponProperties.fireRate; // When can we fire again? (cooldown)
            
            
            StartCoroutine(UpdateCooldownBar(_currentWeaponProperties.fireRate));
        }

    }

    IEnumerator UpdateCooldownBar(float cooldown)
    {
        
        _cooldownSlider.gameObject.SetActive(true);
        while (Time.time < _nextFire)
        {
            _cooldownSlider.maxValue = cooldown;
            _cooldownSlider.value = _nextFire - Time.time;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        _cooldownSlider.gameObject.SetActive(false);
        //_cooldownSlider.value = _cooldownSlider.maxValue; // Reset to 100%
    }
}
