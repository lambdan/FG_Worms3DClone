using UnityEngine;
using UnityEngine.InputSystem;

public class Worm
{
    private string _wormName = Name.GetRandomName();
    private int _teamNumber;

    private GameObject _wormGameObject;
    private PlayerInput _wormPlayerInput;
    private ControlledByAI _wormAIController;
    private WormColor _wormColor;
    private GameManager _gameManager;
    private Health _health;
    private WeaponHolder _weaponHolder;
    private CameraGlue _cameraGlue;
    private AudioSource _audioSource;
    

    // Setters
    public void SetWormName(string newName)
    {
        _wormName = newName;
    }

    public void SetWormGameObject(GameObject newGameObject)
    {
        _wormGameObject = newGameObject;
        _wormPlayerInput = _wormGameObject.GetComponent<PlayerInput>();
        _wormAIController = _wormGameObject.GetComponent<ControlledByAI>();
        _wormColor = _wormGameObject.GetComponent<WormColor>();
        _health = _wormGameObject.GetComponent<Health>();
        _weaponHolder = _wormGameObject.GetComponent<WeaponHolder>();
        _cameraGlue = _wormGameObject.GetComponent<CameraGlue>();
        _audioSource = _wormGameObject.GetComponent<AudioSource>();
    }
    
    public void SetWormColor(Color newColor)
    {
        _wormColor.SetNewColor(newColor);
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
        _weaponHolder.SetGameManager(gameManager);
    }

    public void SetTeamNumber(int newTeamNumber)
    {
        _teamNumber = newTeamNumber;
    }
    

    // Getters
    public string GetWormName()
    {
        return _wormName;
    }

    public bool IsAlive()
    {
        return _health.GetHealth() > 0;
    }

    public int GetTeamNumber()
    {
        return _teamNumber;
    }

    public bool IsDead()
    {
        return _health.GetHealth() <= 0;
    }

    public GameObject GetGameObject()
    {
        return _wormGameObject;
    }

    public Transform GetTransform()
    {
        return GetGameObject().transform;
    }
    
    public PlayerInput GetPlayerInput()
    {
        return _wormPlayerInput;
    }

    public ControlledByAI GetAIController()
    {
        return _wormAIController;
    }

    public WormColor GetWormColor()
    {
        return _wormColor;
    }

    public GameManager GetGameManager()
    {
        return _gameManager;
    }

    public WeaponHolder GetWeaponHolder()
    {
        return _weaponHolder;
    }

    public Health GetHealth()
    {
        return _health;
    }

    public CameraGlue GetCameraGlue()
    {
        return _cameraGlue;
    }

    public AudioSource GetAudioSource()
    {
        return _audioSource;
    }
    
    // State
    public void ActivateAI()
    {
        _wormAIController.enabled = true;
        _wormPlayerInput.enabled = false;
    }

    public void ActivateHumanInput()
    {
        _wormAIController.enabled = false;
        _wormPlayerInput.enabled = true;
    }

    public void Deactivate()
    {
        _wormAIController.enabled = false;
        _wormPlayerInput.enabled = false;
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
