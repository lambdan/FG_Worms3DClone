using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentPlayerText;
    [SerializeField] private TMP_Text _roundsPlayedText;

    public void UpdateCurrentPlayerText(string newText)
    {
        _currentPlayerText.text = newText;
    }

    public void UpdateRoundsPlayed(int rounds)
    {
        _roundsPlayedText.text = "Rounds Played: " + rounds;
    }
}