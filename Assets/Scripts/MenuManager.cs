using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class MenuManager : MenuInputs
{
    [SerializeField] private TMP_Text _humanMenuSelector;
    [SerializeField] private TMP_Text _aiMenuSelector;
    [SerializeField] private TMP_Text _turnTimeSelector;
    [SerializeField] private TMP_Text _wormsPerTeamSelector;

    [SerializeField] private TMP_Text _messageBox;

    private SettingsManager _settingsManager;

    void Awake()
    {
        if (_settingsManager == null) // Only grab the settings manager if there isn't one already going (singleton)
        {
            _settingsManager = FindObjectOfType<SettingsManager>();
        }
    }
    
    void Start()
    {
        newSelection(0);
        RefreshMenu();
    }

    void AttemptStartGame()
    {
        if (_settingsManager.GetTotalPlayers() > _settingsManager.GetMaxPlayers())
        {
            ErrorMessage(
                "There can only be a total of " + _settingsManager.GetMaxPlayers().ToString() + " players.");
            return;
        }
        else if (_settingsManager.GetTotalPlayers() < 2)
        {
            ErrorMessage("There needs to be atleast 2 players");
            return;
        }

        SceneManager.LoadScene("Scenes/PlayScene"); 
    }
    
    public override void Select()
    {
        switch (GetSelectionIndex())
        {
            case 0: AttemptStartGame();
                break;
            case 1: _settingsManager.IncrementHumans();
                break;
            case 2: _settingsManager.IncrementAIs();
                break;
            case 3: _settingsManager.IncrementTurnTime();
                break;
            case 4: _settingsManager.IncrementWorms();
                break;
            case 5: Application.Quit();
                break;
        }
        
        RefreshMenu();
    }

    void RefreshMenu()
    {
        _humanMenuSelector.text = _settingsManager.GetHumans().ToString();
        _aiMenuSelector.text = _settingsManager.GetAIs().ToString();
        _turnTimeSelector.text = _settingsManager.GetTurnLength().ToString();
        _wormsPerTeamSelector.text = _settingsManager.GetWormsPerTeam().ToString();
    }

    void ErrorMessage(string msg)
    {
        _messageBox.color = Color.red;
        _messageBox.text = msg;
        _messageBox.gameObject.SetActive(true);
        StartCoroutine(HideMessage(5));
    }

    IEnumerator HideMessage(float duration)
    {
        float disappear = Time.time + duration;
        while (Time.time < disappear)
        {
            yield return new WaitForSeconds(1);
        }

        _messageBox.gameObject.SetActive(false);
    }
}