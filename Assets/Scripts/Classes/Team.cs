using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private List<Worm> _teamWorms = new List<Worm>();
    private string _teamName = "Unnamed Team";
    private Color _teamColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    private bool _aiControlled = false;
    private int _score = 0;
    private GameObject _gameObject = new GameObject();

    private int _currentWormIndex;

    // Setters
    public void AddWormToTeam(Worm newWorm)
    {
        _teamWorms.Add(newWorm);
    }

    public void SetTeamName(string newName)
    {
        if (newName == "")
        {
            return;
        }
        _teamName = newName;
        _gameObject.name = "Team: " + newName;
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

    // Getters
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

    public GameObject GetGameObject()
    {
        return _gameObject;
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
        int next = _currentWormIndex + 1;
        if (next >= _teamWorms.Count)
        {
            next = 0;
        }

        while (_teamWorms[next].IsAlive() == false)
        {
            next += 1;
            if (next >= _teamWorms.Count)
            {
                next = 0;
            }
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
