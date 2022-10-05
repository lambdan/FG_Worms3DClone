using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatManager : MonoBehaviour
{
    [SerializeField] private List<WeaponProperties> _allWeapons;

    private GameManager _gameManager;
    
    void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }
    
    public void InvincibilityCheat (InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GiveInvincibility();
        }
    }

    public void WeaponsCheat(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GiveAllWeapons();
        }
    }
    
    void GiveAllWeapons()
    {
        foreach (WeaponProperties weaponProperties in _allWeapons)
        {
            _gameManager.GetCurrentWorm().GetWeaponHolder().GetNewWeapon(weaponProperties, 999);
        }
    }

    void GiveInvincibility()
    {
        _gameManager.GetCurrentWorm().GetHealth().StartInvincibility(999);
    }
}
