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
        GiveInvincibility();
    }

    public void WeaponsCheat(InputAction.CallbackContext context)
    {
        GiveAllWeapons();
    }
    
    void GiveAllWeapons()
    {
        foreach (WeaponProperties wp in _allWeapons)
        {
            _gameManager.GetCurrentWorm().GetWeaponHolder().GetNewWeapon(wp, 999);
        }
    }

    void GiveInvincibility()
    {
        _gameManager.GetCurrentWorm().GetHealth().StartInvincibility(999);
    }
}
