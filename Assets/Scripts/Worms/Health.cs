using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DeathHandler))]
public class Health : MonoBehaviour
{
    public int startHealth;
    private int _health;
    private DeathHandler _deathHandler;

    public UnityEvent healthZero;
    public UnityEvent healthChanged;


    void Awake()
    {
        _deathHandler = GetComponent<DeathHandler>();
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
        
        healthChanged.Invoke();

        if (_health <= 0)
        {
            Debug.Log(name + ": Health is <= 0 (I should die)");
            healthZero.Invoke();
        }
    }
    
}
