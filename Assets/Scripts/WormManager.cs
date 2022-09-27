using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HUDUpdater))]
[RequireComponent(typeof(HumanInputListener))]
public class WormManager : MonoBehaviour
{
    [SerializeField] private CameraManager _cameraMan;
    
    private HUDUpdater _HUDUpdater;
    private HumanInputListener _HIL;

    private List<GameObject> _activeWorms = new List<GameObject>();
    private int _activeWormIndex = 0;

    void Awake()
    {
        _HUDUpdater = GetComponent<HUDUpdater>();
        _HIL = GetComponent<HumanInputListener>();
    }

    public void SetActiveTeam(List<GameObject> team)
    {
        _activeWorms = team; // Swap to the new team
        NextWorm(); // BUG: first round always starts with "Worm 2"
    }
    
    void SetActiveWorm(int n)
    {
        // Enable input receiver/AI on this worm
        _activeWorms[n].GetComponent<WormState>().Activate();
        
        // Move camera to this worm
        _cameraMan.SetNewTarget(_activeWorms[n]);
        _cameraMan.Activate();

        // Update name on the HUD
        WormInfo thisWorm = _activeWorms[n].GetComponent<WormInfo>();
        _HUDUpdater.UpdateCurrentPlayerText(thisWorm.GetTeamName() + "/" + thisWorm.GetName());
        
        // Check if worm is NOT AI controlled, if so tell HIL to send its inputs here
        if (_activeWorms[n].GetComponent<WormInfo>().IsAIControlled() == false)
        {
            _HIL.SetNewTarget(_activeWorms[n].GetComponent<InputListener>());
        }
        
        // Update which worm is currently active
        _activeWormIndex = n;
    }

    public GameObject GetCurrentWorm()
    {
        return _activeWorms[_activeWormIndex];
    }

    public void DisableAllActiveWorms()
    {
        foreach (GameObject worm in _activeWorms)
        {
            worm.GetComponent<WormState>().Deactivate();
        }
    }
    
    void DisableWorm(int n)
    {
        _activeWorms[n].GetComponent<WormState>().Deactivate();
    }
    
    public void NextWorm()
    {
        int next = _activeWormIndex + 1;
        if (next >= _activeWorms.Count)
        {
            next = 0;
        }
        // Find the next active & alive worm
        while (!_activeWorms[next].activeSelf && _activeWorms[next].GetComponent<Health>().GetHealth() <= 0)
        {
            next += 1;
            if (next >= _activeWorms.Count)
            {
                next = 0;
            }
        }
        
        DisableWorm(_activeWormIndex);
        SetActiveWorm(next);
    }
    
}
