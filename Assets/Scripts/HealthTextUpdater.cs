using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(DamageTaker))]
public class HealthTextUpdater : MonoBehaviour
{
    private Health _health;
    [SerializeField] TMP_Text _textField;

    private string _currentText;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>(); // Get health object from worm
        Refresh();

    }
    
    public void Refresh()
    {
        _currentText = _health.GetHealth() + " / " + _health.GetMaxHealth();
        _textField.text = _currentText; 
    }
}
