using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class PickupManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pickups;
    [SerializeField] private bool _pickupsEnabled;
    [SerializeField] private float _spawnFrequency;

    private GameManager _gameManager;
    private List<GameObject> _activePickups;
    private List<Vector3> _possibleSpawnLocations;
    private GameObject _pickupParent;

    Vector3 RandomPositionBetween(Vector3 a, Vector3 b)
    {
        Vector3 randomPosition = a + Random.Range(0f, 1f) * (b - a);
        randomPosition.y = a.y + b.y; // Dont mess with height though
        return randomPosition;
    }
    
    void Awake()
    {
        _pickupParent = new GameObject("Pickups");
        _gameManager = GetComponent<GameManager>();
        _activePickups = new List<GameObject>();
        _possibleSpawnLocations = new List<Vector3>();
    }
    
    void Start()
    {
                
        // Generate a list of possible spawn locations based on map's spawn bases
        List<Transform> playerSpawns = _gameManager.GetLevelInfo().GetSpawnBases();
        for (int i = 0; i < (playerSpawns.Count*playerSpawns.Count); i++)
        {
            Vector3 randomPosition = RandomPositionBetween(playerSpawns[Random.Range(0, playerSpawns.Count)].transform.position, playerSpawns[Random.Range(0, playerSpawns.Count)].transform.position);
            _possibleSpawnLocations.Add(randomPosition);
        }
        
        if (_pickupsEnabled && _pickups.Count > 0)
        {
            StartCoroutine(SpawnRandomPickup());
        }
    }
    

    IEnumerator SpawnRandomPickup()
    {
        while (_pickupsEnabled)
        {
            Vector3 pos = _possibleSpawnLocations[Random.Range(0, _possibleSpawnLocations.Count)] + new Vector3(0, 30, 0);
            GameObject go = Instantiate(_pickups[Random.Range(0, _pickups.Count)], pos, Quaternion.identity, _pickupParent.transform);
            _activePickups.Add(go);
            StartCoroutine(LifetimeTimer(go.GetInstanceID(), go.GetComponent<CollisionAction>().GetPickupScript().lifetime));
            yield return new WaitForSeconds(_spawnFrequency);
        }
    }

    public List<GameObject> GetActivePickups()
    {
        return _activePickups;
    }

    IEnumerator LifetimeTimer(int instanceID, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        int index = 0;
        for (int i = 0; i < _activePickups.Count; i++)
        {
            if (_activePickups[i].GetInstanceID() == instanceID)
            {
                index = i;
            }
        }
        
        Destroy(_activePickups[index]);
        _activePickups.RemoveAt(index);
    }

}
