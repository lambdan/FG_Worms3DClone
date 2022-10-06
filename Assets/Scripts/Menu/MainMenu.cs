using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class MainMenu : MenuSystem
{
    [SerializeField] private GameObject _levelPreviewParent;
    
    [SerializeField] private TMP_Dropdown _levelDropdown;
    [SerializeField] private Slider _humanSlider;
    [SerializeField] private TMP_Text _humanNumber;
    [SerializeField] private Slider _aiSlider;
    [SerializeField] private TMP_Text _aiNumber;
    
    
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
        
        // Populate level dropdown
        _levelDropdown.options.Clear();
        foreach (var level in _settingsManager.GetLevels())
        {
            _levelDropdown.options.Add(new TMP_Dropdown.OptionData(text: level.name));
        }

        _levelDropdown.value = _settingsManager.GetLevelIndex();

        _humanSlider.value = _settingsManager.HowManyHumans();
        UpdateHumanAmount();
        _aiSlider.value = _settingsManager.HowManyAIs();
        UpdateAIAmount();
        
        RefreshSliderMinMax();
        
        RefreshMenu();
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        menuSelection.AddListener(Selection);
        menuIncrease.AddListener(Increase);
        menuDecrease.AddListener(Decrease);
    }

    public void RefreshSliderMinMax()
    {
        _humanSlider.wholeNumbers = true;
        _aiSlider.wholeNumbers = true;
        _humanSlider.minValue = 0;
        _aiSlider.minValue = 0;
        _humanSlider.maxValue = _settingsManager.GetMaxPlayers();
        _aiSlider.maxValue = _settingsManager.GetMaxPlayers();
    }

    public void UpdateHumanAmount()
    {
        _settingsManager.ChangeHumanAmount((int)_humanSlider.value);
        _humanNumber.text = _settingsManager.HowManyHumans().ToString();
    }

    public void UpdateAIAmount()
    {
        _settingsManager.ChangeAIAmount((int)_aiSlider.value);
        _aiNumber.text = _settingsManager.HowManyAIs().ToString();
    }

    public void SetLevelIndex(int index)
    {
        _settingsManager.SetLevel(index);
        RefreshSliderMinMax();
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
    
    void Selection()
    {
        if (GetSelectionIndex() == 0)
        {
            AttemptStartGame();
        } else if (GetSelectionIndex() >= 1 && GetSelectionIndex() <= 5)
        {
            Increase();
        } else if (GetSelectionIndex() == 6)
        {
            Application.Quit();
        }
        
        RefreshMenu();
    }

    void Increase()
    {
        switch (GetSelectionIndex())
        {
            case 1: _settingsManager.ChangeLevel(+1);
                break;
            case 2: _settingsManager.ChangeHumanAmount(+1);
                break;
            case 3: _settingsManager.ChangeAIAmount(+1);
                break;
            case 4: _settingsManager.ChangeTurnTime(+5);
                break;
            case 5: _settingsManager.ChangeWormsAmount(+1);
                break; 
        }
        RefreshMenu();
    }

    void Decrease()
    {
        switch (GetSelectionIndex())
        {
            case 1: _settingsManager.ChangeLevel(-1);
                break;
            case 2: _settingsManager.ChangeHumanAmount(-1);
                break;
            case 3: _settingsManager.ChangeAIAmount(-1);
                break;
            case 4: _settingsManager.ChangeTurnTime(-5);
                break;
            case 5: _settingsManager.ChangeWormsAmount(-1);
                break;
        }
        RefreshMenu();
    }
    
    void RefreshMenu()
    {
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
