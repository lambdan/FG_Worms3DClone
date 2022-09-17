using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class HUDUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentPlayerText;
    [SerializeField] private TMP_Text _turnsPlayedText;
    [SerializeField] private Slider _turnTimeSlider;

    public void UpdateCurrentPlayerText(string newText)
    {
        _currentPlayerText.text = newText;
    }
    

    public void UpdateTurnsPlayed(int rounds)
    {
        _turnsPlayedText.text = "Turns played: " + rounds;
    }

    public void UpdateTurnSlider(float current)
    {
        _turnTimeSlider.value = current;
    }

    public void SetTurnSliderMin(float min)
    {
        _turnTimeSlider.minValue = min;
    }

    public void SetTurnSliderMax(float max)
    {
        _turnTimeSlider.maxValue = max;
    }
}