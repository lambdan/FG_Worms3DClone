using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }
    private GameObject[] _levels;
    private GameObject _level;
    private int _levelIndex;
    private LevelInfo _levelInfo;
    
    // Default settings
    private int _humanPlayers = 1;
    private int _aiPlayers = 1;
    private int _turnLength = 20;
    private int _wormsPerTeam = 3;
    private int _maxWormsPerTeam = 20;
    
    private List<string> _playerNames;
    
    public void SetLevel(int newIndex)
    {
        _levelIndex = newIndex;
        _level = _levels[_levelIndex];
        _levelInfo = _level.GetComponent<LevelInfo>();
    }
    
    public void ChangeHumanAmount(int newAmount)
    {
        _humanPlayers = newAmount;
        Debug.Log("Human players is now" + HowManyHumans());
    }

    public void ChangeAIAmount(int newAmount)
    {
        _aiPlayers = newAmount;
        Debug.Log("AI players is now" + HowManyAIs());
    }

    public void ChangeTurnTime(int amount)
    {
        _turnLength += amount;
        if (_turnLength > 100)
        {
            _turnLength = 100;
        }

        if (_turnLength <= 5)
        {
            _turnLength = 5;
        }
    }

    public void ChangeWormsAmount(int amount)
    {
        _wormsPerTeam += amount;
        if (_wormsPerTeam > GetMaxWormsPerTeam())
        {
            _wormsPerTeam = GetMaxWormsPerTeam();
        }

        if (_wormsPerTeam <= 1)
        {
            _wormsPerTeam = 1;
        }
    }

    public void ChangeLevel(int amount)
    {
        _levelIndex += amount;
        if (_levelIndex >= _levels.Length)
        {
            _levelIndex = _levels.Length - 1;
        }

        if (_levelIndex < 0)
        {
            _levelIndex = 0;
        }

        SetLevel(_levelIndex);
    }

    public int HowManyHumans()
    {
        return _humanPlayers;
    }

    public int HowManyAIs()
    {
        return _aiPlayers;
    }

    public int GetTotalPlayers()
    {
        return _aiPlayers + _humanPlayers;
    }

    public int GetMaxPlayers()
    {
        return _levelInfo.SpawnBasesAmount();
    }

    public int GetTurnLength()
    {
        return _turnLength;
    }

    public int GetWormsPerTeam()
    {
        return _wormsPerTeam;
    }

    public int GetMaxWormsPerTeam()
    {
        return _maxWormsPerTeam;
    }
    

    public GameObject GetLevel()
    {
        return _levels[_levelIndex];
    }

    public GameObject[] GetLevels()
    {
        return _levels;
    }

    public int GetLevelIndex()
    {
        return _levelIndex;
    }
    
    public List<string> GetPlayerNames()
    {
        while (_playerNames.Count < HowManyHumans())
        {
            _playerNames.Add("Player " + (_playerNames.Count + 1).ToString());
        }
        return _playerNames;
    }

    public void SetPlayerName(int index, string newName)
    {
        _playerNames[index] = newName;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        _playerNames = new List<string>();
        
        _levels = Resources.LoadAll<GameObject>("Levels"); // Scan for levels
        SetLevel(0);
    }
}