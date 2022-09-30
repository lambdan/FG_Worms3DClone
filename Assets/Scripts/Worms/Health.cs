using System.Collections;
using System.Collections.Generic;
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
        return _health < 0 ? 0 : _health;
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
        // Check if health is full...
        bool wasFull = _health == _maxHealth;

        _maxHealth += amount;

        if (wasFull) // ...  because if it was, set health to new max 
        {
            ChangeHealth(amount);
        }
        
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
            yield return new WaitForSeconds(0.1f);
        }

        wc.SetNewColor(originalColor);
        _invincible = false;
    }


}
