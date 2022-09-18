using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))] 
public class DamageTaker : MonoBehaviour
{
    private Health _health;
    [SerializeField] HealthBar _healthBar;
    
    private bool _hasHealthBar = false;
    private bool _dead = false;
    
    void Awake()
    {
        _health = GetComponent<Health>();
        
        if (_healthBar != null)
        {
            _hasHealthBar = true;
        }
    }
    
    public void TakeDamage(int amount)
    {

        if (_dead)
        {
            return;
        }

        _health.ChangeHealth(-amount);

        if (_hasHealthBar) // Tell the health bar to refresh if we hae one
        {
            _healthBar.Refresh();

            if (_health.GetHealth() <= 0)
            {
                _healthBar.Hide();
            }
            
        }

        if (_health.GetHealth() <= 0)
        {
            _dead = true;
        }
    }
}
