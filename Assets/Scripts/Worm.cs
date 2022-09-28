using UnityEngine;

public class Worm
{
    private string _wormName = "Unknown Worm";
    private bool _alive = true;
    
    private GameObject _wormGameObject;
    private InputListener _wormInputListener;
    private ControlledByAI _wormAIController;

    public void SetWormName(string newName)
    {
        _wormName = newName;
    }

    public void SetAlive(bool newState)
    {
        _alive = newState;
    }

    public void SetWormGameObject(GameObject newGameObject)
    {
        _wormGameObject = newGameObject;
        _wormInputListener = _wormGameObject.GetComponent<InputListener>();
        _wormAIController = _wormGameObject.GetComponent<ControlledByAI>();
    }

    public string GetWormName()
    {
        return _wormName;
    }

    public bool IsAlive()
    {
        return _alive;
    }

    public GameObject GetGameObject()
    {
        return _wormGameObject;
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
}
