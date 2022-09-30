using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private string _levelName;
    [SerializeField] private string _levelDescription;
    
    [Tooltip("Determines how many players can play this map")]
    [SerializeField] private List<Transform> _spawnBases;
    
    [SerializeField] private List<Transform> _dangerZoneLocations;

    public string LevelName()
    {
        return _levelName;
    }

    public string LevelDescription()
    {
        return _levelDescription;
    }
    
    public List<Transform> GetSpawnBases()
    {
        return _spawnBases;
    }

    public List<Transform> GetDangerZones()
    {
        return _dangerZoneLocations;
    }

    public int SpawnBasesAmount()
    {
        return _spawnBases.Count;
    }
    
    public int DangerZoneAmount()
    {
        return _dangerZoneLocations.Count;
    }
}
