using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormManager : MonoBehaviour
{
    [SerializeField] private GameObject _wormPrefab;
    [SerializeField] private int wormsToSpawn;
    
    // So we can quickly test in the editor
    [SerializeField] private bool previousWorm;
    [SerializeField] private bool nextWorm;

    private Camera _camera;

    private List<GameObject> _activeWorms;
    private int _activeWorm = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _activeWorms = new List<GameObject>();
        
        // Spawn n worms // TODO make spawn points 
        for (int i = 0; i < wormsToSpawn; i++)
        {
            Vector3 pos = new Vector3(i*5, 1, i*2);
            GameObject worm = Spawn(_wormPrefab, pos);
            worm.GetComponent<WormState>().Deactivate();
            _activeWorms.Add(worm);
        }
        
        SetActiveWorm(0); // Set first worm as active
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
    }
}
