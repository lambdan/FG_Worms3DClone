using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(WormInfo))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class ControlledByAI : MonoBehaviour
{
    [SerializeField] private float _firingRange;
    
    private WormInfo _wormInfo;
    private Movement _movement;
    private WormManager _wormManager;
    private WeaponHolder _weaponHolder;

    private int _myTeam;
    private List<GameObject> _enemies = new List<GameObject>();
    
    void Awake()
    {
        _wormInfo = GetComponent<WormInfo>();
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _wormManager = FindObjectOfType<WormManager>();
    }

    void Start()
    {
        // Get what team I'm playing for
        _myTeam = _wormInfo.GetTeam();
        
        // Add everyone who's not on my team to my enemy list
        foreach (List<GameObject> team in _wormManager.GetAllTeams())
        {
            foreach (GameObject worm in team)
            {
                if (worm.GetComponent<WormInfo>().GetTeam() != _myTeam)
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
                if (_enemies[i].GetComponent<Health>().GetHealth() <= 0)
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
            if (nearestEnemyDistance > _firingRange)
            {
                _movement.MoveTowards(_enemies[nearestEnemyIndex].transform.position);
            }
            else // Otherwise, start firing
            {
                _weaponHolder.Fire();
            }            
        }
        



    }

}
