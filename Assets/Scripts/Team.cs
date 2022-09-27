using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private List<Worm> _teamWorms = new List<Worm>();
    private string _teamName = "Unnamed Team";
    private Color _teamColor = Color.black;
    private bool _aiControlled = false;
    private int _score = 0;

    public void AddWormToTeam(Worm newWorm)
    {
        _teamWorms.Add(newWorm);
    }

    public void SetTeamName(string newName)
    {
        _teamName = newName;
    }

    public void SetTeamColor(Color newColor)
    {
        _teamColor = newColor;
    }

    public void SetAIControlled(bool newState)
    {
        _aiControlled = newState;
    }

    public void AddScore(int amount)
    {
        _score += amount;
    }

    public List<Worm> GetWorms()
    {
        return _teamWorms;
    }

    public string GetTeamName()
    {
        return _teamName;
    }

    public Color GetTeamColor()
    {
        return _teamColor;
    }

    public bool IsAIControlled()
    {
        return _aiControlled;
    }

    public int GetScore()
    {
        return _score;
    }
}
