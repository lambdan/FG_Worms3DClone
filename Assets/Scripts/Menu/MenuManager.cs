using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MenuInputs
{
    [SerializeField] private TMP_Text _humanMenuSelector;
    [SerializeField] private TMP_Text _aiMenuSelector;
    [SerializeField] private TMP_Text _turnTimeSelector;
    [SerializeField] private TMP_Text _wormsPerTeamSelector;
    [SerializeField] private TMP_Text _messageBox;
    
    [SerializeField] private GameObject _highScoreContainer;

    private SettingsManager _settingsManager;
    private HighScoreManager _highScoreManager;

    void Awake()
    {
        if (_settingsManager == null) // Only grab the settings manager if there isn't one already going (singleton)
        {
            _settingsManager = FindObjectOfType<SettingsManager>();
        }

        _highScoreManager = GetComponent<HighScoreManager>();

    }
    
    void Start()
    {
        _highScoreManager.SetContainer(_highScoreContainer);
        _highScoreManager.PopulateList(_highScoreManager.GetRecords());
        
        newSelection(0); // Focus "Start Game"
        RefreshMenu();
    }

    void AttemptStartGame()
    {
        
        // Make sure we don't have more players than we allow (limited by spawn points)
        if (_settingsManager.GetTotalPlayers() > _settingsManager.GetMaxPlayers())
        {
            ErrorMessage("There can only be a total of " + _settingsManager.GetMaxPlayers().ToString() + " players.");
            return;
        }
        
        // To prevent playing against no one
        if (_settingsManager.GetTotalPlayers() < 2)
        {
            ErrorMessage("There needs to be atleast 2 players");
            return;
        }

        // Load the actual scene... settings gets passed through SettingsManager (doesnt destroy on load)
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
        StartCoroutine(HideMessageAfter(3));
    }

    IEnumerator HideMessageAfter(float duration)
    {
        float disappear = Time.time + duration;
        while (Time.time < disappear)
        {
            yield return new WaitForSeconds(1);
        }

        _messageBox.gameObject.SetActive(false);
    }
}
