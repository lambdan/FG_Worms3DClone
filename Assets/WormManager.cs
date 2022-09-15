using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormManager : MonoBehaviour
{
    [SerializeField] private GameObject _wormPrefab;
    
    
    [SerializeField] private int teamsPlaying;
    [SerializeField] private int wormsPerTeam;
    
    // So we can quickly test in the editor
    [SerializeField] private bool previousWorm;
    [SerializeField] private bool nextWorm;
    [SerializeField] private bool nextTeamTest;

    private Camera _camera;

    private List<GameObject> _activeWorms;
    private List<List<GameObject>> _teams; // Will contain 1 list of worms for each team
    
    private int _activeWorm = 0;
    private int _activeTeam = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
        _activeWorms = new List<GameObject>();
        _teams = new List<List<GameObject>>();

        for (int t = 0; t < teamsPlaying; t++)
        {
            List<GameObject> thisTeamsWorms = new List<GameObject>();
            
            for (int i = 0; i < wormsPerTeam; i++)
            {
                Vector3 pos = new Vector3(i*10, 1, t*20); // TODO make spawn points
                GameObject worm = Spawn(_wormPrefab, pos);
                
                // Set name and team assignment to worm
                WormInfo wormInfo = worm.GetComponent<WormInfo>();
                wormInfo.SetName("Worm " + i); // Worms games had random names... could be fun to put in
                wormInfo.SetTeam(t);
                
                worm.GetComponent<WormState>().Deactivate();
                
                thisTeamsWorms.Add(worm);
            }

            _teams.Add(thisTeamsWorms);
        }

        
        // First team gets first turn
        _activeTeam = 0;
        _activeWorms = _teams[_activeTeam];
        SetActiveWorm(0);
    }

    GameObject Spawn(GameObject go, Vector3 pos)
    {
        return Instantiate(go, pos, Quaternion.identity);
    }

    void SetActiveWorm(int n)
    {
        // Enable input receiver on this worm
        _activeWorms[n].GetComponent<WormState>().Activate();
        
        // Move camera to this worm
        _camera.GetComponent<CameraFollow>().SetNewTarget(_activeWorms[n]);
        
        // Update which worm is currently active
        _activeWorm = n;
    }

    void DisableWorm(int n)
    {
        _activeWorms[n].GetComponent<WormState>().Deactivate();
    }

    void NextTeam()
    {
        if (_teams.Count == 1)
        {
            // No other teams available... could go to game over here?
            return;
        }
        
        int next = _activeTeam + 1;
        if (next == _teams.Count) // Last worm, go back to first
        {
            next = 0; 
        }

        DisableWorm(_activeWorm);
        _activeTeam = next;
        _activeWorms = _teams[_activeTeam]; // Swap out which worms are "active"
        NextWorm();        
    }

    void NextWorm()
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

    void PreviousWorm()
    {
        if (_activeWorms.Count == 1)
        {
            // No other worms available
            return;
        }
        
        int prev = _activeWorm - 1;
        if (prev < 0) // First worm, go back to last
        {
            prev = _activeWorms.Count - 1;
        }
        
        DisableWorm(_activeWorm);
        SetActiveWorm(prev);

    }
    
    // Update is called once per frame
    void Update()
    {
        // These are just for TESTING -- delete them eventually !!
        if (nextWorm)
        {
            NextWorm();
            nextWorm = false;
        }

        if (previousWorm)
        {
            PreviousWorm();
            previousWorm = false;
        }

        if (nextTeamTest)
        {
            NextTeam();
            nextTeamTest = false;
        }
    }

    public List<List<GameObject>> GetAllTeams()
    {
        return _teams;
    }
    
}
