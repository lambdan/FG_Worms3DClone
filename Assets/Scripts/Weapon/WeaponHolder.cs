using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private List<WeaponProperties> _startingWeapons;
    [SerializeField] private Transform _weaponHand;
    [SerializeField] private Slider _cooldownSlider;

    private HUDUpdater _HUDUpdater;
    
    private List<WeaponProperties> _heldWeapons = new List<WeaponProperties>();
    private List<int> _bulletsInClip = new List<int>();
    private List<int> _reserveAmmo = new List<int>();
    
    private int _currentWeaponIndex = 0;
    private WeaponProperties _currentWeaponProperties;
    private GameObject _currentWeaponObject;
    private WeaponScript _currentWeaponScript;

    private float _nextFire = 0;
    
    private float _reloadFinished = 0;
    private Coroutine _reloadCoroutine = null;

    void Awake()
    {
        _HUDUpdater = FindObjectOfType<HUDUpdater>();
        _cooldownSlider.gameObject.SetActive(false); // Hide cooldown slider
    }

    void Start()
    {
        // Give weapons listed in starting weapons
        // TODO this should probably be in GameManager
        foreach (WeaponProperties WP in _startingWeapons)
        {
            GetNewWeapon(WP, WP.clipSize*4); // Start with 2 clips worth of ammo
        }
    }

    public void SwitchWeapon(int index)
    {
        Debug.Log("Switching to weapon " + index);
        
        // Stop reloading (but dont reset bulletsFiredThisClip var)
        StopReload();

        if (_currentWeaponObject != null)
        {
            Destroy(_currentWeaponObject); // Remove current weapon
        }

        _currentWeaponProperties = _heldWeapons[index];
        _currentWeaponIndex = index;
        
        GameObject weapon = Instantiate(_currentWeaponProperties.weaponPrefab, _weaponHand.position,
            _weaponHand.rotation, _weaponHand);
        _currentWeaponObject = weapon;
        _currentWeaponScript = _currentWeaponObject.GetComponent<WeaponScript>();
        _currentWeaponScript.SetWeaponProperties(_currentWeaponProperties);

        _HUDUpdater.UpdateAmmo(_bulletsInClip[index], _reserveAmmo[index]);
    }

    public void GetNewWeapon(WeaponProperties WepProps, int ammo)
    {
        // Check if we already have this weapon
        for (int i = 0; i < _heldWeapons.Count; i++)
        {
            if (_heldWeapons[i].name == WepProps.name)
            {
                // If we do, just add the ammo, dont get the weapon
                _reserveAmmo[i] += ammo;
                _HUDUpdater.UpdateAmmo(_bulletsInClip[i], _reserveAmmo[i]);
                return;
            }
        }
        
        _heldWeapons.Add(WepProps);
        _bulletsInClip.Add(WepProps.clipSize);
        _reserveAmmo.Add(ammo);
        
        // Switch to latest picked up weapon (its gonna be at the end of the list)
        SwitchWeapon(_heldWeapons.Count - 1);
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
        SwitchWeapon(_currentWeaponIndex);

    }
    
    public void Fire()
    {
        if (Time.time > _nextFire && _reloadCoroutine == null && _bulletsInClip[_currentWeaponIndex] > 0)
        {
            _currentWeaponScript.Fire();
            
            _nextFire = Time.time + _currentWeaponProperties.fireRate; // When can we fire again? (cooldown)

            _bulletsInClip[_currentWeaponIndex] -= 1;
            
            _HUDUpdater.UpdateAmmo(_bulletsInClip[_currentWeaponIndex],  _reserveAmmo[_currentWeaponIndex]);
        } else if (_bulletsInClip[_currentWeaponIndex] == 0 && _reserveAmmo[_currentWeaponIndex] > 0)
        {
            // Reload if you try to fire
            TriggerReload();
        }
    }

    public void TriggerReload()
    {
        if (_reloadCoroutine == null && _reserveAmmo[_currentWeaponIndex] > 0)
        {
            _reloadFinished = Time.time + _currentWeaponProperties.reloadSpeed;
            _reloadCoroutine = StartCoroutine(ReloadWeapon(_currentWeaponProperties.reloadSpeed)); 
        }
    }

    void StopReload()
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
            _cooldownSlider.gameObject.SetActive(false); // hide the slider
        }
    }
    
    IEnumerator ReloadWeapon(float reloadTime)
    {
        _cooldownSlider.gameObject.SetActive(true);
        while (Time.time < _reloadFinished)
        {
            _cooldownSlider.maxValue = reloadTime;
            _cooldownSlider.value = _reloadFinished - Time.time;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        int amountNeeded = _currentWeaponProperties.clipSize - _bulletsInClip[_currentWeaponIndex];
        if (_reserveAmmo[_currentWeaponIndex] > amountNeeded)
        {
            // We have enough for a full clip
            _bulletsInClip[_currentWeaponIndex] += amountNeeded;
            _reserveAmmo[_currentWeaponIndex] -= amountNeeded;
        }
        else
        {
            // Not enough for a full clip, take what we can
            _bulletsInClip[_currentWeaponIndex] += _reserveAmmo[_currentWeaponIndex];
            _reserveAmmo[_currentWeaponIndex] = 0;
        }
        
        _HUDUpdater.UpdateAmmo(_bulletsInClip[_currentWeaponIndex],  _reserveAmmo[_currentWeaponIndex]);

        Debug.Log("Reload coroutine finished");
        StopReload();
    }
    
}
