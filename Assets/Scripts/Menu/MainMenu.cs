using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuSystem
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _levelPreviewParent;
    [SerializeField] private TMP_Text _humanMenuSelector;
    [SerializeField] private TMP_Text _aiMenuSelector;
    [SerializeField] private TMP_Text _turnTimeSelector;
    [SerializeField] private TMP_Text _wormsPerTeamSelector;
    [SerializeField] private TMP_Text _messageBox;

    [SerializeField] private GameObject _playerNameRoot;
    [SerializeField] private GameObject _highScoreRoot;

    private SettingsManager _settingsManager;
    private HighScoreManager _highScoreManager;
    private PlayerNameManager _playerNameManager;
    private GameObject _levelPreviewObject;

    void Awake()
    {
        _settingsManager = SettingsManager.Instance;
        if (_settingsManager == null)
        {
            _settingsManager = new GameObject("Settings Manager").AddComponent<SettingsManager>();
        }

        _highScoreManager = GetComponent<HighScoreManager>();
        _playerNameManager = GetComponent<PlayerNameManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = selectionSound;
    }
    
    void Start()
    {
        if (_highScoreManager.GetRecords().highScoreDataList.Count > 0)
        {
            _highScoreManager.PopulateList(_highScoreManager.GetRecords());
        }
        else
        {
            _highScoreRoot.SetActive(false); // Hide high scores if none
        }
        

        newSelection(0); // Focus "Start Game"
        RefreshMenu();
        UnityEngine.Cursor.visible = true;
        menuSelection.AddListener(Selection);
    }

    void AttemptStartGame()
    {
        
        // Make sure we don't have more players than we allow (limited by spawn points)
        if (_settingsManager.GetTotalPlayers() > _settingsManager.GetMaxPlayers())
        {
            ErrorMessage("There can only be a total of " + _settingsManager.GetMaxPlayers().ToString() + " players on this map.");
            return;
        }
        
        // To prevent playing against no one
        if (_settingsManager.GetTotalPlayers() < 2)
        {
            ErrorMessage("There needs to be atleast 2 players");
            return;
        }

        // We can start !
        SceneManager.LoadScene("Scenes/PlayScene");
    }
    
    void Selection()
    {
        switch (GetSelectionIndex())
        {
            case 0: AttemptStartGame();
                break;
            case 1: _settingsManager.IncrementLevel();
                break;
            case 2: _settingsManager.IncrementHumans();
                break;
            case 3: _settingsManager.IncrementAIs();
                break;
            case 4: _settingsManager.IncrementTurnTime();
                break;
            case 5: _settingsManager.IncrementWorms();
                break;
            case 6: Application.Quit();
                break;
        }
        
        RefreshMenu();
    }

    void RefreshMenu()
    {
        UpdateLevelPreview(_settingsManager.GetLevel());
        _levelText.text = _settingsManager.GetLevel().name;
        _humanMenuSelector.text = _settingsManager.HowManyHumans().ToString();
        _aiMenuSelector.text = _settingsManager.HowManyAIs().ToString();
        _turnTimeSelector.text = _settingsManager.GetTurnLength().ToString();
        _wormsPerTeamSelector.text = _settingsManager.GetWormsPerTeam().ToString();

        // Show name input if there are human players
        if (_settingsManager.HowManyHumans() <= 0)
        {
            _playerNameRoot.SetActive(false);
        }
        else
        {
            _playerNameRoot.SetActive(true);
            _playerNameManager.RefreshInputContainer();
        }
    }

    void ErrorMessage(string msg)
    {
        _messageBox.color = Color.red;
        _messageBox.text = msg;
        _messageBox.gameObject.SetActive(true);
        StartCoroutine(ShowErrorMessage(3));
    }

    void UpdateLevelPreview(GameObject newLevel)
    {
        if (_levelPreviewObject == null || (newLevel.gameObject.name != _levelPreviewObject.gameObject.name[0..^7])) // ^7 to remove (Clone) from the name
        {
            Destroy(_levelPreviewObject);
            _levelPreviewObject = Instantiate(_settingsManager.GetLevel(), _levelPreviewParent.transform);
        }
    }

    IEnumerator ShowErrorMessage(float duration)
    {
        bool _highScoreVisibleBefore = _highScoreRoot.activeSelf;
        _highScoreRoot.SetActive(false);
        
        float disappear = Time.time + duration;
        while (Time.time < disappear)
        {
            yield return new WaitForSeconds(1);
        }

        _messageBox.gameObject.SetActive(false);
        
        _highScoreRoot.SetActive(_highScoreVisibleBefore);
    }
}
