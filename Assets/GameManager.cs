using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WormGenerator))]
public class GameManager : MonoBehaviour
{
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

    private int _currentTeamsTurn = 0;
    private float _roundStarted = 0;
    private int _roundsPlayed = 0;
    private bool _delayStarted = false;

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
    
    void StartRound(int team)
    {
        _roundStarted = Time.time;
        _wormManager.SetActiveTeam(_teams[team]);
        _roundsPlayed += 1;
    }

    void NextRound()
    {
        _delayStarted = false;
        Debug.Log("Starting new round!");
        Debug.Log("Rounds played " + _roundsPlayed);
        
        
        // Before we start next round, go through and check who is still alive, and remove the dead ones
        for (int t = 0; t < _teams.Count; t++)
        {
            for (int w = 0; w < _teams[t].Count; w++)
            {
                if (_teams[t][w] == null)
                {
                    _teams[t].RemoveAt(w);
                }
            }
        }
        
        int nextTeam = _currentTeamsTurn + 1;
        if (nextTeam >= _teams.Count)
        {
            nextTeam = 0;
        }

        _currentTeamsTurn = nextTeam;
        StartRound(_currentTeamsTurn);
    }
    
    void Update()
    {
        if (!_delayStarted && Time.time - _roundStarted > roundLength)
        {
            _wormManager.DisableAllActiveWorms();
            StartCoroutine(DelayedStartNextRound(_delayBetweenRounds));
            _delayStarted = true;
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

}
