using UnityEngine;

public class Worm
{
    private string _wormName = "Unknown Worm";
    private bool _alive = true;
    private GameObject _wormGameObject;

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
}
