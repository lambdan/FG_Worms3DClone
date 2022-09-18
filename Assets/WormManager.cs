using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WormManager : MonoBehaviour
{
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private HUDUpdater _HUDUpdater;
    
    private List<GameObject> _activeWorms = new List<GameObject>();
    private int _activeWorm = 0;

    public void SetActiveTeam(List<GameObject> team)
    {
        _activeWorms = team; // Swap to the new team

        for (int i = 0; i < team.Count; i++)
        {
            if (_activeWorms[i].activeSelf && _activeWorms[i].GetComponent<Health>().GetHealth() > 0) // Find an alive worm and make it active
            {
                SetActiveWorm(i); // TODO make it so its not always the 0th worm
                break;
            }
        }
    }
    
    void SetActiveWorm(int n)
    { 
        // Enable input receiver/AI on this worm
        _activeWorms[n].GetComponent<WormState>().Activate();
        
        // Move camera to this worm
        _cameraFollow.SetNewTarget(_activeWorms[n]);
        _cameraFollow.Activate();
        
        // Update name on the HUD
        _HUDUpdater.UpdateCurrentPlayerText(_activeWorms[n].name);
        
        // Update which worm is currently active
        _activeWorm = n;
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
        if (_activeWorms.Count == 1)
        {
            // No other worms available
            return;
        }
        
        int next = _activeWorm + 1;
        if (next == _activeWorms.Count) // Last worm, go back to first
        {
            next = 0; 
        }
        
        DisableWorm(_activeWorm);
        SetActiveWorm(next);
    }
    
}
