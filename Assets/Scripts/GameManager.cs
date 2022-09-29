using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<WeaponProperties> _startingWeapons;
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private GameObject _HUDPrefab;
    [SerializeField] private GameObject _loadingScreenPrefab;
    [SerializeField] private GameObject _pauseMenuPrefab;

    private GameObject _HUD;
    private GameObject _loadingScreen;
    private GameObject _pauseMenu;

    private SettingsManager _settingsManager;
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
        for (int i = 0; i < _settingsManager.HowManyHumans(); i++)
        {
            Team newTeam = GenerateTeam(_settingsManager.GetWormsPerTeam(), _levelInfo.GetSpawnBases()[_teams.Count]);
            newTeam.SetTeamName(_settingsManager.GetPlayerNames()[_teams.Count]);
            foreach (Worm worm in newTeam.GetWorms())
            {
                worm.SetGameManager(this);
                worm.SetWormColor(newTeam.GetTeamColor());
                GiveStartingWeapons(worm);
            }

            _teams.Add(newTeam);
        }

        for (int i = 0; i < _settingsManager.HowManyAIs(); i++)
        {
            Team newTeam = GenerateTeam(_settingsManager.GetWormsPerTeam(), _levelInfo.GetSpawnBases()[_teams.Count]);
            newTeam.SetAIControlled(true);
            newTeam.SetTeamName("AI Team " + (i + 1));
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
        _cameraManager.SetNewTarget(newWorm.GetGameObject(), newWorm.GetCameraGlue());
        
        _hudUpdater.SetTeamText(_currentTeam.GetTeamName());
        _hudUpdater.SetTeamColor(_currentTeam.GetTeamColor());
        _hudUpdater.SetPlayerText(newWorm.GetWormName());
        
        newWorm.GetWeaponHolder().UpdateAmmoHUD();
        
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

    public LevelInfo GetLevelInfo()
    {
        return _levelInfo;
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
        _turnEnds = Time.time + _settingsManager.GetTurnLength();
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
        _hudUpdater.UpdateAliveCount(_teams);
        _currentTeam.AddScore(1000);
        if (TeamsAlive() <= 1)
        {
            GameOver();
        }
    }

    // Game inputs
    public void InputPause(InputAction.CallbackContext context)
    {
        //Debug.Log(context.control.device.name);
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

    public void ControlsChanged(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    // MonoBehaviours

    void Awake()
    {
        _loadingScreen = Instantiate(_loadingScreenPrefab);
        
        _settingsManager = FindObjectOfType<SettingsManager>();
        if (_settingsManager == null)
        {
            _settingsManager = new GameObject("Settings Manager").AddComponent<SettingsManager>();
        }
        
        _pauseMenu = Instantiate(_pauseMenuPrefab);
        _pauseMenu.GetComponent<PauseMenu>().SetGameManager(this);

        _HUD = Instantiate(_HUDPrefab);

        _cameraManager = Camera.main.GetComponent<CameraManager>();
        _pickupManager = GetComponent<PickupManager>();
        _highScoreManager = GetComponent<HighScoreManager>();
        _hudUpdater = _HUD.GetComponent<HUDUpdater>();

        _hudUpdater.SetTurnSliderMax(_settingsManager.GetTurnLength());
    }

    void Start()
    {
        _teams = new List<Team>();
        SetLevel(_settingsManager.GetLevel());
        GenerateTeams();
        _hudUpdater.UpdateAliveCount(_teams);
        _currentTeam = _teams[0];
        StartTurn();
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