using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _levelPreviewParent;
    
    [SerializeField] private TMP_Dropdown _levelDropdown;
    [SerializeField] private Slider _humanSlider;
    [SerializeField] private TMP_Text _humanNumber;
    [SerializeField] private Slider _aiSlider;
    [SerializeField] private TMP_Text _aiNumber;
    [SerializeField] private Slider _turnLengthSlider;
    [SerializeField] private TMP_Text _turnLengthNumber;
    [SerializeField] private Slider _wormsPerTeamSlider;
    [SerializeField] private TMP_Text _wormsPerTeamNumber;
    
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
        
        // Populate level dropdown
        _levelDropdown.options.Clear();
        foreach (var level in _settingsManager.GetLevels())
        {
            _levelDropdown.options.Add(new TMP_Dropdown.OptionData(text: level.name));
        }
        _levelDropdown.value = _settingsManager.GetLevelIndex();

        // Set up sliders
        RefreshSliderMinMax();
        _humanSlider.value = _settingsManager.HowManyHumans();
        _aiSlider.value = _settingsManager.HowManyAIs();
        _turnLengthSlider.value = _settingsManager.GetTurnLength();
        _wormsPerTeamSlider.value = _settingsManager.GetWormsPerTeam();
        
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    private void RefreshSliderMinMax()
    {
        _humanSlider.minValue = 0;
        _humanSlider.maxValue = _settingsManager.GetMaxPlayers();

        _aiSlider.minValue = 0;
        _aiSlider.maxValue = _settingsManager.GetMaxPlayers();
    }

    public void UpdateHumanAmount()
    {
        _settingsManager.ChangeHumanAmount((int)_humanSlider.value);
        _humanNumber.text = _settingsManager.HowManyHumans().ToString();
        
        // Should name entries or not?
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

    public void UpdateAIAmount()
    {
        _settingsManager.ChangeAIAmount((int)_aiSlider.value);
        _aiNumber.text = _settingsManager.HowManyAIs().ToString();
    }

    public void ChangedTurnLength()
    {
        _settingsManager.ChangeTurnLength((int)_turnLengthSlider.value);
        _turnLengthNumber.text = _settingsManager.GetTurnLength().ToString();
    }

    public void ChangedWormsPerTeamAmount()
    {
        _settingsManager.ChangeWormsAmount((int)_wormsPerTeamSlider.value);
        _wormsPerTeamNumber.text = _settingsManager.GetWormsPerTeam().ToString();
    }
    

    public void SetLevelIndex(int index)
    {
        _settingsManager.SetLevel(index);
        RefreshSliderMinMax();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void AttemptStartGame()
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

    void ErrorMessage(string msg)
    {
        _messageBox.color = Color.red;
        _messageBox.text = msg;
        _messageBox.gameObject.SetActive(true);
        StartCoroutine(ShowErrorMessage(3));
    }

    public void UpdateLevelPreview()
    {
        var newLevel = _settingsManager.GetLevel();
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
