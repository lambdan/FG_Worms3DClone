using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormInfo : MonoBehaviour
{
    private string _name;
    private int _team;
    private bool _isAIControlled;
    
    public string GetName()
    {
        return _name;
    }

    public void SetName(string newName)
    {
        _name = newName;

        if (_isAIControlled)
        {
            _name = _name + " [AI]";
        }
        
        UpdateName();
    }

    public int GetTeam()
    {
        return _team;
    }

    public void SetTeam(int newTeam)
    {
        _team = newTeam;
        UpdateName();
    }

    public void UpdateName()
    {
        this.name = "Team " + (_team+1) + " - " + _name;
    }

    public void SetAIControlled(bool state)
    {
        _isAIControlled = state;
    }

    public bool IsAIControlled()
    {
        return _isAIControlled;
    }
    
    
}
