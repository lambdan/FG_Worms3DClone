using System.Collections.Generic;
using UnityEditor;
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
    private int _turnLength = 15;
    private int _wormsPerTeam = 3;
    private int _maxWormsPerTeam = 20;
    
    private List<string> _playerNames;
    
    public void SetLevel(int newIndex)
    {
        _levelIndex = newIndex;
        _level = Instantiate(_levels[newIndex]);
        _levelInfo = _level.GetComponent<LevelInfo>();
    }
    
    public void IncrementHumans()
    {
        if (_humanPlayers >= GetMaxPlayers())
        {
            _humanPlayers = 0;
        }
        else
        {
            _humanPlayers += 1;
        }
    }

    public void IncrementAIs()
    {
        if (_aiPlayers >= GetMaxPlayers())
        {
            _aiPlayers = 0;
        }
        else
        {
            _aiPlayers += 1;
        }
    }

    public void IncrementTurnTime()
    {
        _turnLength += 5;
        if (_turnLength > 100)
        {
            _turnLength = 5;
        }
    }

    public void IncrementWorms()
    {
        _wormsPerTeam += 1;
        if (_wormsPerTeam > GetMaxWormsPerTeam())
        {
            _wormsPerTeam = 1;
        }
    }

    public void IncrementLevel()
    {
        _levelIndex += 1;
        if (_levelIndex >= _levels.Length)
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