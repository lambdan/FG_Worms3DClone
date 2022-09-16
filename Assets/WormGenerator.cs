using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WormGenerator : MonoBehaviour
{
    [SerializeField] private List<string> _names;
    
    
    public List<GameObject> GenerateTeam(GameObject wormPrefab, int amount, int teamNumber, bool aiControlled, Vector3 homebase)
    {
        List<GameObject> thisTeam = new List<GameObject>();
            
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = homebase + new Vector3(i*5, 1, i*5); // TODO make spawn points
            GameObject worm = CreateWorm(wormPrefab, pos);
            
            // Make the worms look towards the center of the map on spawn (keep y the same so they dont look "below")
            worm.transform.LookAt(new Vector3(0, worm.transform.position.y, 0)); 
            
            // Set name and team assignment to worm
            WormInfo wormInfo = worm.GetComponent<WormInfo>();
            wormInfo.SetAIControlled(aiControlled);
            wormInfo.SetName("Worm " + i);
            wormInfo.SetTeam(teamNumber);
            
            worm.GetComponent<WormState>().Deactivate();
                
            thisTeam.Add(worm);
        }

        return thisTeam;
    }

    public GameObject CreateWorm(GameObject wormPrefab, Vector3 spawnPosition) // Is it worth having this separate function? 
    {
        return Spawn(wormPrefab, spawnPosition);
    }
    
    GameObject Spawn(GameObject go, Vector3 pos)
    {
        return Instantiate(go, pos, Quaternion.identity);
    }
}
