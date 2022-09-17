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
    [SerializeField] private GameObject _wormPrefab; // TODO Make this a list with different colored worms? (or maybe just switch texture on them?)
    [SerializeField] private float _delayBetweenRounds;
    [SerializeField] private float _roundLength;
    
    
    private List<List<GameObject>> _teams = new List<List<GameObject>>(); // Holds all living teams
    private List<GameObject> _currentTeam = new List<GameObject>(); // Holds all living worms of the currently active team

    private List<int> _teamAliveWorms = new List<int>();
    private List<string> _teamNames = new List<string>();

    private int _teamsAlive = 0;
    private int _currentTeamsTurn = 0;
    private int _turnsPlayed = 0;
    private int _teamsGenerated = 0;

    private float _turnEnds;
    

    void Awake()
    {
        // Generate teams/worms for human players (aiControlled = false)
        for (int t = 0; t < _humanPlayers; t++)
        {
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, _teamsGenerated, false,
                _homeBases[_teams.Count].position);
            
            _teams.Add(thisTeam);
            _teamAliveWorms.Add(_wormsPerTeam);
            _teamNames.Add("Team " + _teamsGenerated);
            
            _teamsGenerated++;
        }

        // Generate teams/worms for AI players (aiControlled = true)
        for (int t = 0; t < _aiPlayers; t++)
        {
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, _teamsGenerated, true,
                _homeBases[_teams.Count].position);
            
            _teams.Add(thisTeam);
            _teamAliveWorms.Add(_wormsPerTeam);
            _teamNames.Add("Team " + _teamsGenerated);
            
            _teamsGenerated++;
        }

        _teamsAlive = _teamsGenerated;
        
        // Set max of turn time slider to round length
        _HUDUpdater.SetTurnSliderMax(_roundLength);
        
    }

    void Start()
    {
        _currentTeam = _teams[0];
        _currentTeamsTurn = 0;
        StartRound();
    }

    void StartRound()
    {
        _wormManager.SetActiveTeam(_currentTeam);
        _turnsPlayed += 1;
        _turnEnds = Time.time + _roundLength;

        StartCoroutine(RoundTimer());
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
        Debug.Log("Game over!!!!");

        // Get winning team by extracting it from surviving worm's name
        // TODO Make this better
        //var winningTeam = _teams[0][0].name.Substring(0, 6);
        //_HUDUpdater.UpdateCurrentPlayerText("WINNER: " + winningTeam);

        _HUDUpdater.UpdateCurrentPlayerText("GAME OVER");

        Time.timeScale = 0;
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

    public void ReportDeath(GameObject worm, int teamNumber)
    {
        Debug.Log("Game Manager got death report of " + worm.name);

        // Find index of this worm
        int wormIndex = _teams[teamNumber].IndexOf(worm);
        
        // Disable it 
        _teams[teamNumber][wormIndex].SetActive(false);
        
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
        return _roundLength;
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

    IEnumerator RoundTimer()
    {
        while (Time.time <= _turnEnds)
        {
            _HUDUpdater.UpdateTurnSlider(_turnEnds - Time.time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _cameraFollow.Deactivate();
        _wormManager.DisableAllActiveWorms();
        _HUDUpdater.UpdateTurnsPlayed(_turnsPlayed);
        StartCoroutine(DelayedStartNextRound(_delayBetweenRounds));
    }
}