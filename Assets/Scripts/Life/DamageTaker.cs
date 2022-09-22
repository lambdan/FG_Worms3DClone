using UnityEngine;


[RequireComponent(typeof(Health))] 
public class DamageTaker : MonoBehaviour
{
    [SerializeField] private float _invincibilityTime;
    private Health _health;
    private float _lastDamage = 0;

    void Awake()
    {
        _health = GetComponent<Health>();
    }
    
    public void TakeDamage(int amount)
    {
        if (_health.GetHealth() > 0 && (Time.time - _lastDamage > 2f))
        {
            _health.ChangeHealth(-amount);
            _lastDamage = Time.time;
        }

    }
}
