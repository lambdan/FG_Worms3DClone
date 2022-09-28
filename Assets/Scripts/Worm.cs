using UnityEngine;

public class Worm
{
    private string _wormName = "Unknown Worm";

    private GameObject _wormGameObject;
    private InputListener _wormInputListener;
    private ControlledByAI _wormAIController;
    private WormColor _wormColor;
    private Health _health;

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
    }

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

    public void SetWormColor(Color newColor)
    {
        _wormColor.SetNewColor(newColor);
    }
}
