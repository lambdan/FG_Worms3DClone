using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeathHandler))]
public class Health : MonoBehaviour
{
    public int startHealth;
    private int _health;
    private DeathHandler _deathHandler;


    void Awake()
    {
        _deathHandler = GetComponent<DeathHandler>();
    }
    
    void Start()
    {
        _health = startHealth;
    }

    public int GetHealth()
    {
        if (_health < 0)
        {
            return 0;
        }
        return _health;
    }

    public int GetMaxHealth()
    {
        return startHealth;
    }

    public void ChangeHealth(int amount)
    {
        _health += amount;

        if (_health <= 0)
        {
            Debug.Log(name + ": Health is <= 0 (I should die)");
            _deathHandler.Died();
        }
    }
    
}
