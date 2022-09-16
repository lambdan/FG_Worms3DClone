using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentPlayerText;
    [SerializeField] private TMP_Text _roundsPlayedText;
    [SerializeField] private Slider _turnTimeSlider;

    public void UpdateCurrentPlayerText(string newText)
    {
        _currentPlayerText.text = newText;
    }
    

    public void UpdateTurnsPlayed(int rounds)
    {
        _roundsPlayedText.text = "Turns played: " + rounds;
    }

    public void UpdateTurnSlider(float current, float min, float max)
    {
        
    }
}