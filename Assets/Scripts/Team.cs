using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private List<Worm> _teamWorms = new List<Worm>();
    private string _teamName = "Unnamed Team";
    private Color _teamColor = Color.black;
    private bool _aiControlled = false;
    private int _score = 0;

    private int _currentWormIndex;

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

    public Worm CurrentWorm()
    {
        return GetWorm(_currentWormIndex);
    }

    public Worm GetWorm(int index)
    {
        _currentWormIndex = index;
        return _teamWorms[index];
    }

    public Worm GetNextWorm()
    {
        if (AliveWormsInTeam() <= 1)
        {
            return CurrentWorm();
        }

        int next = _currentWormIndex + 1;
        if (next >= _teamWorms.Count)
        {
            next = 0;
        }

        while (_teamWorms[next].IsAlive() == false)
        {
            next += 1;
        }

        return GetWorm(next);
    }

    public int AliveWormsInTeam()
    {
        int alive = 0;
        foreach (Worm worm in _teamWorms)
        {
            if (worm.IsAlive())
            {
                alive += 1;
            }
        }
        return alive;
    }
}
