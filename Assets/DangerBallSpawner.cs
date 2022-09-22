using System.Collections.Generic;
using UnityEngine;

public class DangerBallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _dangerBallPrefab;
    [SerializeField] private int _amount;

    private List<GameObject> _dangerBalls = new List<GameObject>();
    
    void Start()
    {
        for (int i = 0; i < _amount; i++)
        {
            _dangerBalls.Add(SpawnCar());
        }
        
    }

    GameObject SpawnCar()
    {
        GameObject go = Instantiate(_dangerBallPrefab);
        
        go.SetActive(false);

        PatrolRoute thisPatrolCar = go.GetComponent<PatrolRoute>();

        List<Vector3> spawnPoints = thisPatrolCar.GetPatrolPoints();

        int randomPoint = Random.Range(0, spawnPoints.Count);
        
        Vector3 spawnPoint = spawnPoints[randomPoint];

        thisPatrolCar.SetCurrentPoint(randomPoint);

        go.transform.position = new Vector3(spawnPoint.x, spawnPoint.y + 6f, spawnPoint.z); // Move to point, +6 to make it appear a little higher

        go.SetActive(true);

        return go;
    }
    
}
