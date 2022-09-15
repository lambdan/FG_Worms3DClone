using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTextUpdater : MonoBehaviour
{
    private Health _health;
    private TMP_Text _textField;

    private string _currentText;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponentInParent<Health>(); // Get health object from parent worm
        _textField = GetComponent<TMP_Text>();

        _currentText = _health.GetHealth() + " / " + _health.GetMaxHealth();
        _textField.text = _currentText;

    }
    
    public void Refresh()
    {
        _currentText = _health.GetHealth() + " / " + _health.GetMaxHealth();
        _textField.text = _currentText; 
    }
}
