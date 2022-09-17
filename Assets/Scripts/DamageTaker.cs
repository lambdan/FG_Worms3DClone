using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))] 
public class DamageTaker : MonoBehaviour
{
    private Health _health;
    private HealthTextUpdater _healthTextUpdater;
    [SerializeField] HealthBar _healthBar;
    
    private bool _hasHealthText = false;
    private bool _hasHealthBar = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        _health = GetComponent<Health>();
        _healthTextUpdater = GetComponent<HealthTextUpdater>();
        
        if (_healthTextUpdater != null)
        {
            _hasHealthText = true;
        }

        if (_healthBar != null)
        {
            _hasHealthBar = true;
        }
    }
    
    public void TakeDamage(int amount)
    {
        _health.ChangeHealth(-amount);
        // Refresh floating text above worm if it has one
        if (_hasHealthText)
        {
            _healthTextUpdater.Refresh(); 
        }

        if (_hasHealthBar)
        {
            _healthBar.Refresh();
        }
    }
}
