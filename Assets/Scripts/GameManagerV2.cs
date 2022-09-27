using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameManagerV2 : MonoBehaviour
{
    // Settings (these should be gotten through settings manager)
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private int humans = 1;
    [SerializeField] private int ais = 1;
    [SerializeField] private int perteam = 3;
    [SerializeField] private List<string> teamnames;
    [SerializeField] private List<Color> teamcolors;
    // End settings
    
    [SerializeField] private List<string> _wormNames;
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private GameObject _loadingScreenPrefab;

    private GameObject _loadingScreen;

    private CameraManager _cameraManager;
    
    private GameObject _level;
    private LevelInfo _levelInfo;

    private List<Team> _teams;
    private Team _currentTeam;
    private Worm _currentWorm;

    void SetLevel(GameObject levelPrefab)
    {
        _level = Instantiate(levelPrefab);
        _levelInfo = _level.GetComponent<LevelInfo>();
    }
    
    Team GenerateTeam(int amount, Transform homebase)
    {
        Team team = new Team();
        float spawnAngle = (360f / amount); // Used for spawning in a circle around base
        
        for (int i = 0; i < amount; i++)
        {
            Worm worm = new Worm();
            worm.SetWormName(_wormNames[Random.Range(0, _wormNames.Count)]);

            Vector3 spawnPoint = homebase.position + 6 * new Vector3(Mathf.Cos(spawnAngle * i), 0, Mathf.Sin(spawnAngle * i));
            worm.SetWormGameObject(Instantiate(_wormPrefab, spawnPoint, Quaternion.identity));
            worm.GetGameObject().transform.LookAt(Vector3.zero); // To make them look toward the center

            team.AddWormToTeam(worm);
        }
        return team;
    }

    void GenerateTeams()
    {
        for (int i = 0; i < humans; i++)
        {
            Team newTeam = GenerateTeam(perteam, _levelInfo.GetSpawnBases()[_teams.Count]);
            newTeam.SetTeamName(teamnames[_teams.Count]);
            newTeam.SetTeamColor(teamcolors[_teams.Count]);
            _teams.Add(newTeam);
        }

        for (int i = 0; i < ais; i++)
        {
            Team newTeam = GenerateTeam(perteam, _levelInfo.GetSpawnBases()[_teams.Count]);
            newTeam.SetAIControlled(true);
            newTeam.SetTeamName("AI Team " + (i + 1));
            newTeam.SetTeamColor(teamcolors[_teams.Count]);
            _teams.Add(newTeam); 
        }
    }
    
    void Awake()
    {
        _cameraManager = Camera.main.GetComponent<CameraManager>();
    }

    void Start()
    {
        _loadingScreen = Instantiate(_loadingScreenPrefab);
        
        _teams = new List<Team>();
        SetLevel(_levelPrefab);
        GenerateTeams();

        _currentTeam = _teams[0];
        _currentWorm = _currentTeam.GetWorms()[0];

        _cameraManager.SetNewTarget(_currentWorm.GetGameObject());
        
        Destroy(_loadingScreen);
    }
    
}
