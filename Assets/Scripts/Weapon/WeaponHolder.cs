using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform _weaponHand;
    [SerializeField] private Slider _reloadBar;

    private GameManager _gameManager;
    
    private List<int> _bulletsInClip = new List<int>();
    private List<int> _reserveAmmo = new List<int>();

    private int _currentWeaponIndex;
    
    private List<GameObject> _weaponObjects = new List<GameObject>();
    private List<WeaponScript> _weaponScripts = new List<WeaponScript>();
    private List<WeaponProperties> _heldWeapons = new List<WeaponProperties>();

    private float _nextFire;

    private float _reloadFinished;
    private Coroutine _reloadCoroutine;

    void Awake()
    {
        _reloadBar.gameObject.SetActive(false);
    }

    public void SwitchWeapon(int newIndex)
    {
        // Stop reloading (but dont reset bulletsFiredThisClip var)
        StopReload();
        _nextFire = 0;

        // Deactivate old weapon
        _weaponObjects[_currentWeaponIndex].SetActive(false);

        // Set new weapon
        _currentWeaponIndex = newIndex;
        _weaponObjects[_currentWeaponIndex].transform.position = _weaponHand.position;
        _weaponObjects[_currentWeaponIndex].transform.rotation = _weaponHand.rotation;
        _weaponObjects[_currentWeaponIndex].SetActive(true);
        
        UpdateAmmoHUD();
    }

    public void GetNewWeapon(WeaponProperties newWeaponProperties, int ammo)
    {
        // Check if we already have this weapon
        for (int i = 0; i < _heldWeapons.Count; i++)
        {
            if (_heldWeapons[i].name == newWeaponProperties.name)
            {
                // If we do, just add the ammo, dont get the weapon
                _reserveAmmo[i] += ammo;
                UpdateAmmoHUD();
                return;
            }
        }
        
        _heldWeapons.Add(newWeaponProperties);
        _bulletsInClip.Add(newWeaponProperties.clipSize);
        _reserveAmmo.Add(ammo);

        GameObject weapon = Instantiate(newWeaponProperties.weaponPrefab, _weaponHand.transform); // weapon hand as parent
        weapon.SetActive(false);

        WeaponScript weaponScript = weapon.GetComponent<WeaponScript>();
        weaponScript.SetWeaponProperties(newWeaponProperties);
        
        _weaponObjects.Add(weapon);
        _weaponScripts.Add(weapon.GetComponent<WeaponScript>());

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
        
        SwitchWeapon(next);

    }
    
    public void Fire()
    {
        if (Time.time > _nextFire && _reloadCoroutine == null && _bulletsInClip[_currentWeaponIndex] > 0)
        {
            _weaponScripts[_currentWeaponIndex].Fire();
            
            _nextFire = Time.time + _heldWeapons[_currentWeaponIndex].fireRate; // When can we fire again? (cooldown)

            _bulletsInClip[_currentWeaponIndex] -= 1;
            
            UpdateAmmoHUD();
        } else if (_bulletsInClip[_currentWeaponIndex] == 0 && _reserveAmmo[_currentWeaponIndex] > 0)
        {
            // Reload if you try to fire
            TriggerReload();
        }
    }

    public void TriggerReload()
    {
        if (_bulletsInClip[_currentWeaponIndex] == _heldWeapons[_currentWeaponIndex].clipSize)
        {
            return; // Avoid reloading when clip is already full
        }
        
        if (_reloadCoroutine == null && _reserveAmmo[_currentWeaponIndex] > 0)
        {
            _reloadFinished = Time.time + _heldWeapons[_currentWeaponIndex].reloadSpeed;
            _reloadCoroutine = StartCoroutine(ReloadWeapon(_heldWeapons[_currentWeaponIndex].reloadSpeed)); 
        }
    }

    void StopReload()
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
            _reloadBar.gameObject.SetActive(false); // hide the slider
        }
    }

    public bool HasAmmoInAnyWeapon()
    {
        foreach (int i in _bulletsInClip)
        {
            if (i > 0)
            {
                return true;
            }
        }
        
        foreach (int i in _reserveAmmo)
        {
            if (i > 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasAmmoInThisWeapon()
    {
        if (TotalAmmoThisWeapon(_currentWeaponIndex) > 0)
        {
            return true;
        }

        return false;
    }

    int TotalAmmoThisWeapon(int index)
    {
        return _bulletsInClip[index] + _reserveAmmo[index];
    }
    
    IEnumerator ReloadWeapon(float reloadTime)
    {
        _weaponScripts[_currentWeaponIndex].PlayReloadSound();
        _reloadBar.gameObject.SetActive(true);
        while (Time.time < _reloadFinished)
        {
            _reloadBar.maxValue = reloadTime;
            _reloadBar.value = _reloadFinished - Time.time;
            yield return new WaitForEndOfFrame();
        }

        int amountNeeded = _heldWeapons[_currentWeaponIndex].clipSize - _bulletsInClip[_currentWeaponIndex];
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
        
        UpdateAmmoHUD();
        StopReload();
    }

    public void UpdateAmmoHUD()
    {
        _gameManager.GetHUDUpdater().UpdateAmmo(_bulletsInClip[_currentWeaponIndex],  _reserveAmmo[_currentWeaponIndex]);
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }


}
