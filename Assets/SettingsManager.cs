using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private int _maxPlayers;
    [SerializeField] private int _humanPlayers;
    [SerializeField] private int _aiPlayers;

    private int totalPlayers()
    {
        return _humanPlayers + _aiPlayers;
    }

    public void IncrementHumans()
    {
        if (_humanPlayers >= 8)
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
        if (_aiPlayers >= 8)
        {
            _aiPlayers = 0;
        }
        else
        {
            _aiPlayers += 1;
        }
    }

    public int GetHumans()
    {
        return _humanPlayers;
    }

    public int GetAIs()
    {
        return _aiPlayers;
    }

    public int GetTotalPlayers()
    {
        return _aiPlayers + _humanPlayers;
    }

    public int GetMaxPlayers()
    {
        return _maxPlayers;
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
