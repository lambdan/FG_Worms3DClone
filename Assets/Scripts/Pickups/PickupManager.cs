using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pickups;
    [SerializeField] private float _spawnFrequency;
    
    private List<GameObject> _activePickups = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        if (_pickups.Count > 0)
        {
            StartCoroutine(SpawnRandomPickup());
        }
    }
    

    IEnumerator SpawnRandomPickup()
    {
        while (true)
        {
            Vector3 pos = new Vector3(Random.Range(-40, 40), 2.5f, Random.Range(-40, 40));
            GameObject go = Instantiate(_pickups[Random.Range(0, _pickups.Count)], pos, Quaternion.identity);
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
