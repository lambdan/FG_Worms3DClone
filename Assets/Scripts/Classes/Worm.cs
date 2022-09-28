using UnityEngine;

public class Worm
{
    private string _wormName = "Unknown Worm";

    private GameObject _wormGameObject;
    private InputListener _wormInputListener;
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
        _wormInputListener = _wormGameObject.GetComponent<InputListener>();
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
    
    public InputListener GetInputListener()
    {
        return _wormInputListener;
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
    
    // State
    public void ActivateAI()
    {
        _wormAIController.enabled = true;
        _wormInputListener.enabled = false;
    }

    public void ActivateHumanInput()
    {
        _wormAIController.enabled = false;
        _wormInputListener.enabled = true;
    }

    public void Deactivate()
    {
        _wormAIController.enabled = false;
        _wormInputListener.enabled = false;
    }
}
