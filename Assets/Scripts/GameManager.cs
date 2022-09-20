using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HUDUpdater))]
[RequireComponent(typeof(WormGenerator))]
[RequireComponent(typeof(WormManager))]
[RequireComponent(typeof(HumanInputListener))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _homeBases;
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private float _delayBetweenTurns;
    [SerializeField] private List<Color> teamColors;

    private HUDUpdater _HUDUpdater;
    private CameraFollow _cameraFollow;
    private WormManager _wormManager;
    private WormGenerator _wormGenerator;
    private HumanInputListener _HIL;
    
    private SettingsManager _settingsManager;
    
    private List<List<GameObject>> _teams = new List<List<GameObject>>(); // Holds all living teams
    private List<GameObject> _currentTeam = new List<GameObject>(); // Holds all living worms of the currently active team

    private List<int> _teamAliveWorms = new List<int>();
    private List<string> _teamNames = new List<string>();

    private int _teamsAlive = 0;
    private int _currentTeamsTurn = 0;
    private int _turnsPlayed = 0;
    private int _teamsGenerated = 0;

    private int _humanPlayers = 1; // Default settings for testing... gets overriden if we have a settings manager
    private int _aiPlayers = 1;
    private int _turnLength = 60; 
    private int _wormsPerTeam = 10;
    
    private bool _gameOver = false;

    private float _turnEnds;

    void Awake()
    {
        _wormManager = GetComponent<WormManager>();
        _HUDUpdater = GetComponent<HUDUpdater>();
        _wormGenerator = GetComponent<WormGenerator>();
        _HIL = GetComponent<HumanInputListener>();
        _cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }
    
    void GenerateTeams(int humans, int ais)
    {
        // Generate teams/worms for human players (aiControlled = false)
        for (int t = 0; t < humans; t++)
        {
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, _teamsGenerated, false,
                _homeBases[_teams.Count].position, teamColors[_teamsGenerated]);
            
            _teams.Add(thisTeam);
            _teamAliveWorms.Add(_wormsPerTeam);
            _teamNames.Add("Team " + _teamsGenerated);
            
            _teamsGenerated++;
        }

        // Generate teams/worms for AI players (aiControlled = true)
        for (int t = 0; t < ais; t++)
        {
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, _teamsGenerated, true,
                _homeBases[_teams.Count].position, teamColors[_teamsGenerated]);
            
            _teams.Add(thisTeam);
            _teamAliveWorms.Add(_wormsPerTeam);
            _teamNames.Add("Team " + _teamsGenerated);
            
            _teamsGenerated++;
        }

        _teamsAlive = _teamsGenerated;
    }
    
    void Start()
    {
        _settingsManager = FindObjectOfType<SettingsManager>();
        if (_settingsManager != null)
        {
            // We have a settings manager, use its settings
            _humanPlayers = _settingsManager.GetHumans();
            _aiPlayers = _settingsManager.GetAIs();
            _turnLength = _settingsManager.GetTurnLength();
            _wormsPerTeam = _settingsManager.GetWormsPerTeam();
        }

        GenerateTeams(_humanPlayers, _aiPlayers);
        
        _currentTeam = _teams[0];
        _currentTeamsTurn = 0;
        
        // Set max of turn time slider to round length
        _HUDUpdater.SetTurnSliderMax(_turnLength);
        
        StartRound();
    }

    void StartRound()
    {
        _wormManager.SetActiveTeam(_currentTeam);
        _turnsPlayed += 1;
        _turnEnds = Time.time + _turnLength;
        StartCoroutine(TurnTimer());
    }

    void NextRound()
    {
        int next = GetNextTeam();
        _currentTeamsTurn = next;
        _currentTeam = _teams[_currentTeamsTurn];
        StartRound();
    }

    int GetNextTeam()
    {
        int nextTeam = _currentTeamsTurn + 1;
        if (nextTeam >= _teams.Count)
        {
            nextTeam = 0;
        }
        
        while (_teamAliveWorms[nextTeam] == 0)
        {
            nextTeam += 1;
            if (nextTeam >= _teams.Count)
            {
                nextTeam = 0;
            }
        }

        return nextTeam;
    }

    void GameOver()
    {
        _gameOver = true;
        _cameraFollow.Deactivate();
        _wormManager.DisableAllActiveWorms();
        _HUDUpdater.UpdateCurrentPlayerText("GAME OVER");
        StartCoroutine(GameOverDelay());
    }
    void TeamDefeated(int t)
    {
        Debug.Log("Team " + t + " is defeated");
        _teamsAlive -= 1;

        if (_teamsAlive == 1)
        {
            GameOver();
        }

    }

    public void ReportDeath(int teamNumber)
    {
        // -1 alive worms of that team
        _teamAliveWorms[teamNumber] -= 1;
        
        // Check if everyone is dead on that team
        if (_teamAliveWorms[teamNumber] == 0)
        {
            TeamDefeated(teamNumber);
        }
    }

    public float GetRoundLength()
    {
        return _turnLength;
    }

    public List<List<GameObject>> GetAllTeams()
    {
        return _teams;
    }

    IEnumerator DelayedStartNextTurn(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextRound();
    }

    IEnumerator TurnTimer()
    {
        while (Time.time <= _turnEnds)
        {
            _HUDUpdater.UpdateTurnSlider(_turnEnds - Time.time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        // Turn over
        _cameraFollow.Deactivate();
        _wormManager.DisableAllActiveWorms();
        _HUDUpdater.UpdateTurnsPlayed(_turnsPlayed);

        if (!_gameOver)
        {
            StartCoroutine(DelayedStartNextTurn(_delayBetweenTurns));
        }

    }

    IEnumerator GameOverDelay()
    {
        float duration = Time.time + 3;
        while (Time.time < duration)
        {
            yield return new WaitForSeconds(1);
        }

        SceneManager.LoadScene("Scenes/Menu");
    }
}