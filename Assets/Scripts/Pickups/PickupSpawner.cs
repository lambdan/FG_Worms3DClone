using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pickups;
    [SerializeField] private float _spawnFrequency;
    
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
            GameObject go = _pickups[Random.Range(0, _pickups.Count)];
            Instantiate(go, pos, Quaternion.identity);
            yield return new WaitForSeconds(_spawnFrequency);
        }
    }
}
