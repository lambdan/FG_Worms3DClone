using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentTeamText;
    [SerializeField] private TMP_Text _currentPlayerText;
    [SerializeField] private TMP_Text _roundsPlayedText;

    // Start is called before the first frame update
    void Awake()
    {
        _currentPlayerText.text = "Sup from HUD Updater";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateCurrentPlayerText(string newText)
    {
        _currentPlayerText.text = newText;
    }

    public void UpdateTeamText(int teamNumber)
    {
        // Add +1 here to get team 1 instead of team 0?
        _currentTeamText.text = "Team " + teamNumber;
    }

    public void UpdateRoundsPlayed(int rounds)
    {
        _roundsPlayedText.text = "Rounds Played: " + rounds;
    }
}