using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HUDUpdater))]
[RequireComponent(typeof(WormGenerator))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private HUDUpdater _HUDUpdater;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private List<Transform> _homeBases;
    [SerializeField] private int _humanPlayers;
    [SerializeField] private int _aiPlayers;
    [SerializeField] private int _wormsPerTeam;
    [SerializeField] private WormManager _wormManager;
    [SerializeField] WormGenerator _wormGenerator;
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private float _delayBetweenTurns;
    [SerializeField] private float _turnLength;
    [SerializeField] private List<Color> teamColors;
    
    private List<List<GameObject>> _teams = new List<List<GameObject>>(); // Holds all living teams
    private List<GameObject> _currentTeam = new List<GameObject>(); // Holds all living worms of the currently active team

    private List<int> _teamAliveWorms = new List<int>();
    private List<string> _teamNames = new List<string>();

    private int _teamsAlive = 0;
    private int _currentTeamsTurn = 0;
    private int _turnsPlayed = 0;
    private int _teamsGenerated = 0;

    private bool _gameOver = false;

    private float _turnEnds;
    
    void GenerateTeams()
    {
        // Generate teams/worms for human players (aiControlled = false)
        for (int t = 0; t < _humanPlayers; t++)
        {
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, _teamsGenerated, false,
                _homeBases[_teams.Count].position, teamColors[_teamsGenerated]);
            
            _teams.Add(thisTeam);
            _teamAliveWorms.Add(_wormsPerTeam);
            _teamNames.Add("Team " + _teamsGenerated);
            
            _teamsGenerated++;
        }

        // Generate teams/worms for AI players (aiControlled = true)
        for (int t = 0; t < _aiPlayers; t++)
        {
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, _teamsGenerated, true,
                _homeBases[_teams.Count].position, teamColors[_teamsGenerated]);
            
            _teams.Add(thisTeam);
            _teamAliveWorms.Add(_wormsPerTeam);
            _teamNames.Add("Team " + _teamsGenerated);
            
            _teamsGenerated++;
        }

        _teamsAlive = _teamsGenerated;
        
        // Set max of turn time slider to round length
        _HUDUpdater.SetTurnSliderMax(_turnLength);
    }
    
    void Start()
    {
        // TODO Get amount of humans/AI's here in a menu
        GenerateTeams();
        
        _currentTeam = _teams[0];
        _currentTeamsTurn = 0;
        
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
        Debug.Log("Game over!!!!");
        _HUDUpdater.UpdateCurrentPlayerText("GAME OVER");
        
        // TODO go to a menu or something here?
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
}