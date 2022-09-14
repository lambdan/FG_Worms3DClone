using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startHealth = 100;
    private int _health = 100;
    
    void Start()
    {
        _health = startHealth;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void ChangeHealth(int amount)
    {
        _health += amount;

        if (_health <= 0)
        {
            Debug.Log(name + ": Health is <= 0 (I should die)");
        }
    }
    
}
