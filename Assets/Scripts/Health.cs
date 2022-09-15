using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startHealth;
    private int _health;
    
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
            
            GetComponent<DeathAnimation>().TriggerDeathAnimation(); // TODO A nicer way to do this
            
        }
    }
    
}
