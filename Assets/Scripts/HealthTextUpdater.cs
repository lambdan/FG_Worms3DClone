using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(DamageTaker))]
public class HealthTextUpdater : MonoBehaviour
{
    private Health _health;
    [SerializeField] TextMeshPro _textField;

    private string _currentText;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>(); // Get health object from worm
        if (_health.GetHealth() > 50)
        {
            _textField.color = Color.green;
        }
        else
        {
            _textField.color = Color.red;
        }
        Refresh();

    }
    
    public void Refresh()
    {
        _currentText = _health.GetHealth() + " / " + _health.GetMaxHealth();
        _textField.text = _currentText; 
    }
}
