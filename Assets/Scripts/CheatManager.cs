using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    [SerializeField] private List<WeaponProperties> _allWeapons;

    private GameManager _gameManager;
    
    // Update is called once per frame
    void Awake()
    {
        _gameManager = GetComponent<GameManager>();
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
            _gameManager.GetCurrentWorm().GetWeaponHolder().GetNewWeapon(wp, 999);
        }
    }

    void GiveInvincibility()
    {
        _gameManager.GetCurrentWorm().GetHealth().StartInvincibility(999);
    }
}
