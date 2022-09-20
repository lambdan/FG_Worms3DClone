using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [SerializeField] private int _maxPlayers;

    private int _humanPlayers = 1;
    private int _aiPlayers = 1;
    private int _turnLength = 15;
    private int _wormsPerTeam = 3;

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
        if (_wormsPerTeam > 10)
        {
            _wormsPerTeam = 1;
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

    public int GetTurnLength()
    {
        return _turnLength;
    }

    public int GetWormsPerTeam()
    {
        return _wormsPerTeam;
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
    }
}