using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    // Settings (these should be gotten through settings manager)
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private int humans = 1;
    [SerializeField] private int ais = 1;
    [SerializeField] private int perteam = 3;
    [SerializeField] private float _turnLength;
    [SerializeField] private List<string> teamnames;

    [SerializeField] private List<Color> teamcolors;
    // End settings

    [SerializeField] private List<string> _wormNames;
    [SerializeField] private List<WeaponProperties> _startingWeapons;
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private GameObject _HUDPrefab;
    [SerializeField] private GameObject _loadingScreenPrefab;
    [SerializeField] private GameObject _pauseMenuPrefab;

    private GameObject _HUD;
    private GameObject _loadingScreen;
    private GameObject _pauseMenu;

    private CameraManager _cameraManager;
    private PickupManager _pickupManager;
    private HighScoreManager _highScoreManager;
    private HUDUpdater _hudUpdater;

    private GameObject _level;
    private LevelInfo _levelInfo;

    private List<Team> _teams;
    private Team _currentTeam;
    private int _currentTeamIndex;
    private Worm _currentWorm;

    private int _turnsPlayed;
    private float _turnEnds;

    public UnityEvent _deathEvent = new UnityEvent();

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

            Vector3 spawnPoint = homebase.position +
                                 6 * new Vector3(Mathf.Cos(spawnAngle * i), 0, Mathf.Sin(spawnAngle * i));
            worm.SetWormGameObject(Instantiate(_wormPrefab, spawnPoint, Quaternion.identity));
            worm.GetTransform().LookAt(Vector3.zero); // To make them look toward the center

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
                worm.SetGameManager(this);
                worm.SetWormColor(newTeam.GetTeamColor());
                GiveStartingWeapons(worm);
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
                worm.SetGameManager(this);
                worm.SetWormColor(newTeam.GetTeamColor());
                GiveStartingWeapons(worm);

                worm.GetAIController().SetGameManager(this);
                worm.GetAIController().SetPickupManager(_pickupManager);
                worm.GetAIController().SetTeam(newTeam);
            }

            _teams.Add(newTeam);
        }
    }

    void GiveStartingWeapons(Worm worm)
    {
        foreach (WeaponProperties weapon in _startingWeapons)
        {
            worm.GetWeaponHolder().GetNewWeapon(weapon, 2 * weapon.clipSize);
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
        }
    }

    // Getters
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

    public Worm GetCurrentWorm()
    {
        return _currentWorm;
    }

    public HUDUpdater GetHUDUpdater()
    {
        return _hudUpdater;
    }

    // Turn based
    Team NextTeam()
    {
        int next = _teams.IndexOf(_currentTeam) + 1;
        if (next >= _teams.Count)
        {
            next = 0;
        }

        while (_teams[next].AliveWormsInTeam() <= 0)
        {
            next += 1;
        }

        return _teams[next];
    }

    void UpdateTurnsPlayed()
    {
        _turnsPlayed += 1;
        _hudUpdater.UpdateTurnsPlayed(_turnsPlayed);
    }

    void SetNewWorm(Worm worm)
    {
        _currentWorm = worm;
        FocusNewWorm(_currentWorm);
    }

    void StartTurn()
    {
        UpdateTurnsPlayed();
        SetNewWorm(_currentTeam.GetNextWorm());
        _turnEnds = Time.time + _turnLength;
        StartCoroutine(TurnTimer());
    }

    void NextTurn()
    {
        _currentTeam = NextTeam();
        StartTurn();
    }

    void TurnEnd()
    {
        _cameraManager.Deactivate();
        DeactivateCurrentWorm();

        if (TeamsAlive() > 1)
        {
            StartCoroutine(DelayedStartNextTurn(2f));
        }
    }

    void CancelTurn()
    {
        _turnEnds = Time.time;
    }

    void GameOver()
    {
        _cameraManager.Deactivate();
        DeactivateCurrentWorm();
        _highScoreManager.RecordNewScore(_currentTeam.GetTeamName(), _currentTeam.GetScore());
        StartCoroutine(GameOverDelay());
    }

    int TeamsAlive()
    {
        int alive = 0;
        foreach (Team team in _teams)
        {
            if (team.AliveWormsInTeam() > 0)
            {
                alive += 1;
            }
        }

        return alive;
    }

    // Deaths and high score
    public void DeathReport()
    {
        Debug.Log("Death report");
        _currentTeam.AddScore(1000);
        if (TeamsAlive() <= 1)
        {
            GameOver();
        }
    }

    // Game inputs
    public void InputPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TogglePause();
        }
    }

    public void InputNextWorm(InputAction.CallbackContext context)
    {
        if (_currentTeam.IsAIControlled())
        {
            return; // So humans cant change worm for the AI 
        }
        
        if (context.started)
        {
            NextWorm();
        }
    }

    // MonoBehaviours

    void Awake()
    {
        _loadingScreen = Instantiate(_loadingScreenPrefab);
        _pauseMenu = Instantiate(_pauseMenuPrefab);
        _pauseMenu.GetComponent<PauseMenu>().SetGameManager(this);

        _HUD = Instantiate(_HUDPrefab);

        _cameraManager = Camera.main.GetComponent<CameraManager>();
        _pickupManager = GetComponent<PickupManager>();
        _highScoreManager = GetComponent<HighScoreManager>();
        _hudUpdater = _HUD.GetComponent<HUDUpdater>();

        _hudUpdater.SetTurnSliderMax(_turnLength);
    }

    void Start()
    {
        _teams = new List<Team>();
        SetLevel(_levelPrefab);
        GenerateTeams();

        _currentTeam = _teams[0];
        StartTurn();

        _deathEvent.AddListener(DeathReport);

        Destroy(_loadingScreen);
    }

    IEnumerator TurnTimer()
    {
        while (Time.time <= _turnEnds)
        {
            _hudUpdater.UpdateTurnSlider(_turnEnds - Time.time);
            yield return new WaitForFixedUpdate();
        }

        TurnEnd();
    }

    IEnumerator DelayedStartNextTurn(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextTurn();
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