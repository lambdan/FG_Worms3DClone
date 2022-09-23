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

    private List<WeaponProperties> _heldWeapons = new List<WeaponProperties>();
    private int _currentWeaponIndex = 0;
    private WeaponProperties _currentWeaponProperties;
    private GameObject _currentWeaponObject;
    private WeaponScript _currentWeaponScript;

    private float _nextFire = 0;
    private bool _cooldownPulsing = false;

    void Awake()
    {
        _cooldownSlider.gameObject.SetActive(false); // Hide cooldown slider
    }

    void Start()
    {
        foreach (WeaponProperties WP in _startingWeapons)
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
                //Debug.Log("We already have this weapon... not picking it up again");
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

            if (_currentWeaponProperties.fireRate >= 0.5)
            {   
                // Show cooldown bar if its a long one
                StartCoroutine(UpdateCooldownBar(_currentWeaponProperties.fireRate));
            }
            
        }
        else
        {
            // Pulse the cooldown
            if (!_cooldownPulsing)
            {
                StartCoroutine(PulseCooldownBar());
            }
            
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
    }

    IEnumerator PulseCooldownBar()
    {
        _cooldownPulsing = true;
        Vector3 startScale = _cooldownSlider.transform.localScale;
        
        // First make it bigger
        while (_cooldownSlider.transform.localScale.x <= (startScale.x * 2f))
        {
            _cooldownSlider.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForFixedUpdate();
        }
        
        // Then make it smaller
        while (_cooldownSlider.transform.localScale.x >= (startScale.x))
        {
            _cooldownSlider.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForFixedUpdate();
        }
        
        _cooldownSlider.transform.localScale = startScale;
        _cooldownPulsing = false;
    }
}
