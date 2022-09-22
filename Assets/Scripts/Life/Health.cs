using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int startHealth;
    private int _health;
    private int _maxHealth;
    private bool _invincible = false;

    public UnityEvent healthZero;
    public UnityEvent healthChanged;
    
    void Awake()
    {
        _health = startHealth;
        _maxHealth = startHealth;
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
        bool wasFull = false;
        if (_health == _maxHealth)
        {
            wasFull = true;
        }
        
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
        Color[] colors = new Color[] { Color.blue, Color.yellow, Color.green, Color.red };
        
        WormColor wc = GetComponent<WormColor>();
        Color originalColor = wc.GetColor();

        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            wc.SetNewColor(colors[Random.Range(0,colors.Length)]);
            yield return new WaitForSeconds(0.1f);
        }

        wc.SetNewColor(originalColor);
        _invincible = false;
    }


}
