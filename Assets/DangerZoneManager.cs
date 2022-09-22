using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneManager : MonoBehaviour
{
    [SerializeField] private GameObject _dangerZonePrefab;
    [SerializeField] private List<Transform> _dangerZoneLocations;
    [SerializeField] private float _switchAfter;

    private int _locationIndex = 0;
    private float _nextSwitch = 0;
    private GameObject _currentDZ;

    void Start()
    {
        _locationIndex = Random.Range(0, _dangerZoneLocations.Count); // Randomly select first location
        _currentDZ = Instantiate(_dangerZonePrefab, _dangerZoneLocations[_locationIndex].position, Quaternion.identity);
        StartCoroutine(DangerZoneTimer());
    }

    int NextLocationIndex()
    {
        int next = _locationIndex + 1;
        if (next >= _dangerZoneLocations.Count)
        {
            next = 0; // Back to first
        }

        return next;
    }
    
    IEnumerator DangerZoneTimer()
    {
        yield return new WaitForSeconds(_switchAfter);
        _locationIndex = NextLocationIndex();
        _currentDZ.transform.position = _dangerZoneLocations[_locationIndex].position;
        StartCoroutine(DangerZoneTimer());
    }

}
