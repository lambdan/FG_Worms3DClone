using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject _HUDPrefab;
    [SerializeField] private GameObject _loadingScreenPrefab;
    [SerializeField] private GameObject _pauseMenuPrefab;

    private GameObject _HUD;
    private GameObject _loadingScreen;
    private GameObject _pauseMenu;

    private CameraManager _cameraManager;
    private PickupManager _pickupManager;
    private HumanInputListener _humanInputListener;
    private HUDUpdater _hudUpdater;

    private GameObject _level;
    private LevelInfo _levelInfo;

    private List<Team> _teams;
    private Team _currentTeam;
    private Worm _currentWorm;

    // Level initialization
    
    void SetLevel(GameObject levelPrefab)
    {
        _level = Instantiate(levelPrefab);
        _levelInfo = _level.GetComponent<LevelInfo>();
    }
    
    // Team generation
    
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
            foreach (Worm worm in newTeam.GetWorms())
            {
                worm.SetWormColor(newTeam.GetTeamColor());
            }
            _teams.Add(newTeam);
        }

        for (int i = 0; i < ais; i++)
        {
            Team newTeam = GenerateTeam(perteam, _levelInfo.GetSpawnBases()[_teams.Count]);
            newTeam.SetAIControlled(true);
            newTeam.SetTeamName("AI Team " + (i + 1));
            newTeam.SetTeamColor(teamcolors[_teams.Count]);
            foreach (Worm worm in newTeam.GetWorms())
            {
                worm.SetWormColor(newTeam.GetTeamColor());
                worm.GetAIController().SetGameManager(this);
                worm.GetAIController().SetPickupManager(_pickupManager);
                worm.GetAIController().SetTeam(newTeam);
            }
            _teams.Add(newTeam); 
        }
    }
    
    // Pausing
    
    public void TogglePause()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);

    }
    
    // Worm switching

    public void NextWorm()
    {
        DeactivateCurrentWorm();
        _currentWorm = _currentTeam.GetNextWorm();
        FocusNewWorm(_currentWorm);
    }

    void DeactivateCurrentWorm()
    {
        _currentWorm.Deactivate();
        _humanInputListener.DisableTarget();
    }
    
    void FocusNewWorm(Worm newWorm)
    {
        _cameraManager.SetNewTarget(newWorm.GetGameObject());
        if (_currentTeam.IsAIControlled())
        {
            newWorm.ActivateAI();
        }
        else
        {
            newWorm.ActivateHumanInput();
            _humanInputListener.SetNewTarget(newWorm.GetInputListener());
        }
    }

    // AI will ask for enemies
    public List<Worm> GetAliveEnemiesOfTeam(Team myTeam)
    {
        List<Worm> aliveEnemies = new List<Worm>();
        foreach (Team team in _teams)
        {
            if (team == myTeam)
            {
                continue;
            }

            foreach (Worm worm in team.GetWorms())
            {
                if (worm.IsAlive())
                {
                    aliveEnemies.Add(worm);
                }
            }
        }
        return aliveEnemies;
    }

    // MonoBehaviours
    
    void Awake()
    {
        _loadingScreen = Instantiate(_loadingScreenPrefab);
        _pauseMenu = Instantiate(_pauseMenuPrefab);
        _HUD = Instantiate(_HUDPrefab);
        
        _cameraManager = Camera.main.GetComponent<CameraManager>();
        _pickupManager = GetComponent<PickupManager>();
        _humanInputListener = GetComponent<HumanInputListener>();
        _hudUpdater = _HUD.GetComponent<HUDUpdater>();
    }

    void Start()
    {
        _teams = new List<Team>();
        SetLevel(_levelPrefab);
        GenerateTeams();

        _currentTeam = _teams[0];
        _currentWorm = _currentTeam.GetWorm(0);
        FocusNewWorm(_currentWorm);
        
        Destroy(_loadingScreen);
    }
    
}
