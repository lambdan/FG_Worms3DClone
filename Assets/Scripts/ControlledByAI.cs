using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(WormInfo))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class ControlledByAI : MonoBehaviour
{

    private WormInfo _wormInfo;
    private Movement _movement;
    private GameManager _gameManager;
    private WeaponHolder _weaponHolder;

    private int _myTeam;
    private List<GameObject> _enemies = new List<GameObject>();
    
    void Awake()
    {
        _wormInfo = GetComponent<WormInfo>();
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        // Get what team I'm playing for
        _myTeam = _wormInfo.GetTeam();
        
        // Add everyone who's not on my team to my enemy list
        foreach (List<GameObject> team in _gameManager.GetAllTeams())
        {
            foreach (GameObject worm in team)
            {
                if (worm != null && worm.GetComponent<WormInfo>().GetTeam() != _myTeam)
                {
                    _enemies.Add(worm);
                }
            }
        }
    }

    void Update()
    {
        if (_enemies.Count > 0)
        {
            int nearestEnemyIndex = 0;
            float nearestEnemyDistance = 0;
            float distance;
        
            // Find nearest enemy
            for (int i = 0; i < _enemies.Count; i++)
            {
                // Check if that enemy is still alive, and remove it from our enemy list if not
                if (_enemies[i] == null || _enemies[i].GetComponent<Health>().GetHealth() <= 0)
                {
                    _enemies.RemoveAt(i);
                    continue;
                }
            
                distance = Vector3.Distance(transform.position, _enemies[i].transform.position);
                if (i == 0 || distance < nearestEnemyDistance)
                {
                    nearestEnemyDistance = distance;
                    nearestEnemyIndex = i;
                }
            }
        
            // If we are far away from the enemy, move closer
            if (nearestEnemyDistance > Random.Range(5, 10))
            {
                _movement.MoveTowards(_enemies[nearestEnemyIndex].transform.position);
            }
            else // Otherwise, start firing
            {
                _weaponHolder.Fire();
            }            
        }
        else
        {
            // No enemies left... move around a little for the fun of it
            _movement.MoveTowards(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
        }

        if (Random.Range(0, 10000) == 0)
        {
            // Switch weapons occasionally
            _weaponHolder.NextWeapon();
        }


    }

}
