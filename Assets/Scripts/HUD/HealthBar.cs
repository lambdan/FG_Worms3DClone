using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private Animation _heartAnimation;
    [SerializeField] private TMP_Text _text;

    void Start()
    {
        SetMax(_health.GetMaxHealth());
        SetMin(0);
        Refresh();
    }

    public void StartPulsing()
    {
        _heartAnimation.Play();
    }
    
    public void StopPulsing()
    {
        _heartAnimation.Stop();
    }

    public void Hide()
    {
        _slider.gameObject.SetActive(false);
    }
    
    public void SetMax(float max)
    {
        _slider.maxValue = max;
    }

    public void SetMin(float min)
    {
        _slider.minValue = min;
    }

    public void Refresh()
    {
        _slider.value = _health.GetHealth();
        _text.text = _health.GetHealth().ToString();
    }
}
