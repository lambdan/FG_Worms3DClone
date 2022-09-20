using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int startHealth;
    private int _health;

    public UnityEvent healthZero;
    public UnityEvent healthChanged;
    
    void Awake()
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
        
        healthChanged.Invoke();

        if (_health <= 0)
        {
            healthZero.Invoke();
        }
    }
    
}
