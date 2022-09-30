/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HUDUpdater))]
[RequireComponent(typeof(HumanInputListener))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _homeBases;
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private float _delayBetweenTurns;
    [SerializeField] private List<Color> teamColors;
    [SerializeField] private PauseMenu _pauseMenu;
    
    [Header("Test Settings")]
    [Tooltip("These are only used when starting the PlayScene directly")]
    [SerializeField] private int _humanPlayers = 1; // Default settings for testing... gets overriden if we have a settings manager
    [SerializeField] private int _aiPlayers = 1;
    [SerializeField] private int _turnLength = 30; 
    [SerializeField] private int _wormsPerTeam = 2;

    private HUDUpdater _HUDUpdater;
    private CameraManager _cameraMan;
    private HumanInputListener _HIL;
    private SettingsManager _settingsManager;
    private HighScoreManager _highScoreManager;
    private List<int> _scores = new List<int>();

    private List<List<GameObject>> _teams = new List<List<GameObject>>(); // Holds all living teams
    private List<GameObject> _currentTeam = new List<GameObject>(); // Holds all living worms of the currently active team

    
    private List<int> _teamAliveWorms = new List<int>();
    private List<string> _humanNames = new List<string> { "a", "b", "c", "d", "e", "f", "g" };
    private List<string> _teamNames = new List<string>();

    private int _teamsAlive = 0;
    private int _currentTeamsTurn = 0;
    private int _turnsPlayed = 0;
    private int _teamsGenerated = 0;
    
    private bool _gameOver = false;
    
    private float _turnEnds; // When the turn will end (time), used in the timer coroutine

    void Awake()
    {
        _settingsManager = FindObjectOfType<SettingsManager>();
        if (_settingsManager != null)
        {
            // We have a settings manager (because player came from the main menu) - use its settings
            _humanPlayers = _settingsManager.GetHumans();
            _aiPlayers = _settingsManager.GetAIs();
            _turnLength = _settingsManager.GetTurnLength();
            _wormsPerTeam = _settingsManager.GetWormsPerTeam();
            _humanNames = _settingsManager.GetHumanNames();
        }
        
        _wormManager = GetComponent<WormManager>();
        _HUDUpdater = GetComponent<HUDUpdater>();
        _wormGenerator = GetComponent<WormGenerator>();
        _HIL = GetComponent<HumanInputListener>();
        _cameraMan = Camera.main.GetComponent<CameraManager>();
        _highScoreManager = GetComponent<HighScoreManager>();
        Time.timeScale = 1; // Important to set here because you might come from a paused game -> main menu -> new game
    }
    
    void GenerateTeams(int humans, int ais)
    {
        // Generate teams/worms for human players (aiControlled = false)
        for (int t = 0; t < humans; t++)
        {
            string teamName = _humanNames[t];
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, _teamsGenerated, false,
                _homeBases[_teams.Count].position, teamColors[_teamsGenerated], teamName);
            
            _teams.Add(thisTeam);
            _teamAliveWorms.Add(_wormsPerTeam);
            _teamNames.Add(teamName);
            
            _teamsGenerated++;
        }

        // Generate teams/worms for AI players (aiControlled = true)
        for (int t = 0; t < ais; t++)
        {
            string teamName = "AI Team " + (t + 1);
            List<GameObject> thisTeam = _wormGenerator.GenerateTeam(_wormPrefab, _wormsPerTeam, _teamsGenerated, true,
                _homeBases[_teams.Count].position, teamColors[_teamsGenerated], teamName);
            
            _teams.Add(thisTeam);
            _teamAliveWorms.Add(_wormsPerTeam);
            _teamNames.Add(teamName);
            
            _teamsGenerated++;
        }

        _teamsAlive = _teamsGenerated;
        
        // Init scores
        for (int t = 0; t < _teamsGenerated; t++)
        {
            _scores.Add(0);
        }
    }
    
    void Start()
    {
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
        _HUDUpdater.UpdateTurnsPlayed(_turnsPlayed);
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
        if (nextTeam >= _teams.Count) // Last team - go back to first
        {
            nextTeam = 0;
        }
        
        while (_teamAliveWorms[nextTeam] == 0) // While we are on a team that is dead
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
        _cameraMan.Deactivate();
        _wormManager.DisableAllActiveWorms();
        _HIL.DisableTarget();
        _HUDUpdater.UpdateCurrentPlayerText("Game Over!");
        
        // Record high score for the team that won
        _highScoreManager.RecordNewScore(_teamNames[_currentTeamsTurn], _scores[_currentTeamsTurn]);

        StartCoroutine(GameOverDelay()); // Start delay before going back to the main menu
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
        // Award team that was active with the kill
        _scores[_currentTeamsTurn] += 1000;
        Debug.Log("team " + _currentTeamsTurn + " now has " + _scores[_currentTeamsTurn] + " points");
        
        // -1 alive worms of that team
        _teamAliveWorms[teamNumber] -= 1;
        
        // Check if everyone is dead on that team
        if (_teamAliveWorms[teamNumber] == 0)
        {
            TeamDefeated(teamNumber);
        }
        
        // If that death was on current team, end turn
        if (teamNumber == _currentTeamsTurn)
        {
            CancelRound();
        }
        

    }

    public void TogglePause()
    {
        /*
        if (_paused)
        {
            // Unpause
            _pauseMenu.Deactivate();
            Time.timeScale = 1;
            _paused = false;
        }
        else
        {
            // Pause
            _pauseMenu.Activate();
            Time.timeScale = 0;
            _paused = true;
        }*/
/*
    }

    public void CancelRound()
    {
        _turnEnds = Time.time; // Ugly hack
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
        _cameraMan.Deactivate();
        _wormManager.DisableAllActiveWorms();
        _HIL.DisableTarget();

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

        SceneManager.LoadScene("Scenes/Menu"); // Go back to main menu
    }
}
*/