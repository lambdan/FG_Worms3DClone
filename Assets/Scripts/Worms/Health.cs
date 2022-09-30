using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int startHealth;
    private int _health;
    private int _maxHealth;
    private bool _invincible;
    
    public UnityEvent healthZero;
    public UnityEvent healthChanged;
    
    void Awake()
    {
        _health = startHealth;
        _maxHealth = startHealth;
    }

    public int GetHealth()
    {
        return _health < 0 ? 0 : _health; // Returns 0 if its <0 
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        if (_invincible && amount < 0)
        {
            // Invincible + its a negative number (damage): ignore 
            return;
        }
        
        _health += amount;
        
        if (_health <= 0)
        {
            healthZero.Invoke();
        }

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        
        healthChanged.Invoke();
    }

    public void ChangeMaxHealth(int amount)
    {
        _maxHealth += amount;
        _health += amount;
        healthChanged.Invoke();
    }

    public void StartInvincibility(float duration)
    {
        if (!_invincible)
        {
            _invincible = true;
            StartCoroutine(InvincibilityTimer(duration));
        }
    }

    IEnumerator InvincibilityTimer(float duration)
    {
        WormColor wc = GetComponent<WormColor>();
        Color originalColor = wc.GetColor();
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            wc.SetNewColor(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
            yield return new WaitForFixedUpdate();
        }
        wc.SetNewColor(originalColor); // Restore color
        _invincible = false;
    }


}
