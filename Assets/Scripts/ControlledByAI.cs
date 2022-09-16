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
    private bool _startedMoving;
    private bool _startedShooting;

    void Awake()
    {
        _wormInfo = GetComponent<WormInfo>();
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        Debug.Log(this.name + "AI START");
        
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
        
        _startedMoving = false;
        _startedShooting = false;
        StartCoroutine(WaitBeforeMoving(Random.Range(1, 3))); // Simulate a thinking period
    }

    void FixedUpdate()
    {
        if (!_startedMoving)
        {
            return;
        }
        
        if (_enemies.Count > 0)
        {
            GameObject nearestEnemy = null;
            float nearestEnemyDistance = 0;
            float distance;

            foreach (GameObject enemy in _enemies)
            {
                if (enemy == null) // this should catch dead enemies
                {
                    continue;
                }
                
                distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (nearestEnemy == null || distance < nearestEnemyDistance)
                {
                    nearestEnemyDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        
            // If we havent started shooting and we are far away from the enemy move towards it
            if (!_startedShooting && nearestEnemyDistance > Random.Range(7, 12))
            {
                _movement.MoveTowards(nearestEnemy.transform.position);
            }
            else // Otherwise, start firing
            {
                _startedShooting = true;
                _movement.RotateTowards(nearestEnemy.transform.position);
                
                // Simulate random presses of the trigger
                if (Random.Range(0, 100) < 10)
                {
                    _weaponHolder.Fire();
                }
                
            }            
        }
        
        if (Random.Range(0, 10000) == 0)
        {
            // Switch weapons occasionally
            _weaponHolder.NextWeapon();
        }


    }

    IEnumerator WaitBeforeMoving(float delay)
    {
        yield return new WaitForSeconds(delay);
        _startedMoving = true;
    }
}
