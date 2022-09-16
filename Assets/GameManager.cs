using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WormGenerator))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private List<Transform> _homeBases;
    [SerializeField] private int _humanPlayers;
    [SerializeField] private int _aiPlayers;
    [SerializeField] private int _wormsPerTeam;
    [SerializeField] private WormManager _wormManager;
    [SerializeField] WormGenerator _wormGenerator;
    [SerializeField] private GameObject _wormPrefab; // TODO Make this a list with different colored worms? (or maybe just switch texture on them?)
    [SerializeField] private float _delayBetweenRounds;
    [SerializeField] private float roundLength;
    
    // Generate teams in WormGenerator
    // Pass them to WormManager
    // Handle game state here
    // Send commands to WormManager and have them manage which worms are active etc.
    
    private List<List<GameObject>> _teams = new List<List<GameObject>>();
    private List<GameObject> _currentTeam = new List<GameObject>();

    private int _currentTeamsTurn = 0;
    private int _roundsPlayed = 0;
    private bool _gameOver = false;

    void Awake()
    {
        // Generate teams/worms for human players (aiControlled = false)
        for (int t = 0; t < _humanPlayers; t++)
        {
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, t, false, _homeBases[_teams.Count].position);
            _teams.Add(thisTeam);
        }
        
        // Generate teams/worms for AI players (aiControlled = true)
        for (int t = 0; t < _aiPlayers; t++)
        {
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, t, true, _homeBases[_teams.Count].position);
            _teams.Add(thisTeam);
        }
    }

    void Start()
    {
        _currentTeamsTurn = 0;
        _currentTeam = _teams[_currentTeamsTurn];
        StartCoroutine(DelayedStartNextRound(_delayBetweenRounds));
    }

    void StartRound(int team)
    {
        _wormManager.SetActiveTeam(_currentTeam);
        _roundsPlayed += 1;
        StartCoroutine(RoundTimer(roundLength));
    }

    void NextRound()
    {
        Debug.Log("Starting new round!");
        Debug.Log("Rounds played " + _roundsPlayed);

        int currentTeamIndex = _teams.IndexOf(_currentTeam);
        
        int nextTeam = currentTeamIndex + 1;
        if (nextTeam >= _teams.Count)
        {
            nextTeam = 0;
        }

        _currentTeamsTurn = nextTeam;
        _currentTeam = _teams[_currentTeamsTurn];
        StartRound(_currentTeamsTurn);
    }

    void GameOver()
    {
        Debug.Log("Game over!!!!");
        Time.timeScale = 0;
    }
    
    void RemoveTeam(int t)
    {
        Debug.Log("Removing team " + t);
        _teams.RemoveAt(t);

        if (_teams.Count == 1)
        {
            _wormManager.DisableAllActiveWorms();
            GameOver();
        }
    }
    public void ReportDeath(GameObject worm, int teamNumber)
    {
        Debug.Log("Game Manager got death report of " + worm.name);
        
        // Find index of this worm
        int wormIndex = _teams[teamNumber].IndexOf(worm);
            
        // Destroy it and remove it from the team
        Destroy(_teams[teamNumber][wormIndex]);
        _teams[teamNumber].RemoveAt(wormIndex);

        if (_teams[teamNumber].Count == 0)
        {
            RemoveTeam(teamNumber);
        }
    }
    
    public List<List<GameObject>> GetAllTeams()
    {
        return _teams;
    }

    IEnumerator DelayedStartNextRound(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextRound();
    }

    IEnumerator RoundTimer(float roundLength)
    {
        yield return new WaitForSeconds(roundLength);
        _cameraFollow.Deactivate();
        _wormManager.DisableAllActiveWorms();
        StartCoroutine(DelayedStartNextRound(_delayBetweenRounds));
    }

}
