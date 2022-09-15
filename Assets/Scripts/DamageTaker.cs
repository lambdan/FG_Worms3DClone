using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))] 
public class DamageTaker : MonoBehaviour
{
    private Health _health;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
    }

    public void TakeDamage(int amount)
    {
        _health.ChangeHealth(-amount);
    }
}
