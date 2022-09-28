using UnityEngine;
using UnityEngine.InputSystem;

public class Worm
{
    private string _wormName = "Unknown Worm";

    private GameObject _wormGameObject;
    private PlayerInput _wormPlayerInput;
    private ControlledByAI _wormAIController;
    private WormColor _wormColor;
    private GameManagerV2 _gameManager;
    private Health _health;
    private WeaponHolder _weaponHolder;

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
    }
    
    public void SetWormColor(Color newColor)
    {
        _wormColor.SetNewColor(newColor);
    }

    public void SetGameManager(GameManagerV2 gameManager)
    {
        _gameManager = gameManager;
        _weaponHolder.SetGameManager(gameManager);
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

    public GameManagerV2 GetGameManager()
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
}
