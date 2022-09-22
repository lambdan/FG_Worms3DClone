using UnityEngine;


[RequireComponent(typeof(Health))] 
public class DamageTaker : MonoBehaviour
{
    private Health _health;

    void Awake()
    {
        _health = GetComponent<Health>();
    }
    
    public void TakeDamage(int amount)
    {
        if (_health.GetHealth() > 0)
        {
            _health.ChangeHealth(-amount);
        }

    }
}
