using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class DangerZoneManager : MonoBehaviour
{
    [SerializeField] private GameObject _dangerZonePrefab;
    private List<Transform> _dangerZoneLocations;
    private float _switchAfter = 30f;
    [SerializeField] private bool _randomOrder;

    private int _locationIndex;
    private GameObject _dangerZoneParent;
    private GameObject _dangerZone;

    private Coroutine _dangerZoneTimer;

    void Awake()
    {
        _dangerZoneParent = new GameObject("Danger Zones");
    }
    
    int NewLocation()
    {
        if (_randomOrder)
        {
            return Random.Range(0, _dangerZoneLocations.Count);
        }
        else
        {
            int next = _locationIndex + 1;
            if (next >= _dangerZoneLocations.Count)
            {
                next = 0;
            }

            return next;
        }
    }

    public void SetLocations(List<Transform> locations)
    {
        _dangerZoneLocations = locations;
    }

    public List<Transform> GetLocations()
    {
        return _dangerZoneLocations;
    }

    public void Activate()
    {
        _locationIndex = NewLocation();
        _dangerZone = Instantiate(_dangerZonePrefab, _dangerZoneLocations[_locationIndex].position, Quaternion.identity, _dangerZoneParent.transform);
        _dangerZoneTimer = StartCoroutine(DangerZoneTimer());
    }

    public void Deactivate()
    {
        StopCoroutine(_dangerZoneTimer);
        Destroy(_dangerZone);
    }

    public void SetTime(float newTime)
    {
        _switchAfter = newTime;
    }
    
    IEnumerator DangerZoneTimer()
    {
        yield return new WaitForSeconds(_switchAfter);
        _locationIndex = NewLocation();
        _dangerZone.transform.position = _dangerZoneLocations[_locationIndex].position;
        StartCoroutine(DangerZoneTimer());
    }

}
