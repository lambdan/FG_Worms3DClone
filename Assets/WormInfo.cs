using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormInfo : MonoBehaviour
{
    [SerializeField] private Transform _cameraGlue;

    private string _name;
    private int _team;
    private bool _isAIControlled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string newName)
    {
        _name = newName;
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
        this.name = "Team " + _team + " - " + _name;
    }

    public void SetAIControlled(bool state)
    {
        _isAIControlled = state;
    }

    public bool IsAIControlled()
    {
        return _isAIControlled;
    }

    public Transform GetCameraGlue()
    {
        return _cameraGlue.transform;
    }
    
}
