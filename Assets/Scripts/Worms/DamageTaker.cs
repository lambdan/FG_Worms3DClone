using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Health))] 
public class DamageTaker : MonoBehaviour
{
    [SerializeField] private float _invincibilityTime;
    private Health _health;
    private float _lastDamage = 0;
    public UnityEvent tookDamage;

    void Awake()
    {
        _health = GetComponent<Health>();
    }
    
    public void TakeDamage(int amount)
    {
        if (_health.GetHealth() > 0 && (Time.time - _lastDamage > _invincibilityTime))
        {
            tookDamage.Invoke();
            _health.ChangeHealth(-amount);
            _lastDamage = Time.time;
        }

    }
}
