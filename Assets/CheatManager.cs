using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    [SerializeField] private List<WeaponProperties> _allWeapons;
    
    private WormManager _WM;
    
    // Update is called once per frame
    void Awake()
    {
        _WM = GetComponent<WormManager>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GiveInvincibility();
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GiveAllWeapons();
        }
    }

    void GiveAllWeapons()
    {
        foreach (WeaponProperties wp in _allWeapons)
        {
            _WM.GetCurrentWorm().GetComponent<WeaponHolder>().GetNewWeapon(wp, 999);
        }
    }

    void GiveInvincibility()
    {
        _WM.GetCurrentWorm().GetComponent<Health>().StartInvincibility(999);
    }
}
