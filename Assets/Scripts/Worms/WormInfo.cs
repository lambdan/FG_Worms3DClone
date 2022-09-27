using UnityEngine;

public class WormInfo : MonoBehaviour
{
    private string _name;
    private string _teamName;
    private int _team;
    private bool _isAIControlled;
    
    public string GetName()
    {
        return _name;
    }

    public string GetTeamName()
    {
        return _teamName;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }

    public int GetTeam()
    {
        return _team;
    }

    public void SetTeam(int newTeam)
    {
        _team = newTeam;
    }

    public void SetTeamName(string newTeamName)
    {
        _teamName = newTeamName;
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
