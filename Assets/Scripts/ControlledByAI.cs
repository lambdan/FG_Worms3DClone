using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(WormInfo))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class ControlledByAI : MonoBehaviour
{
    private WormInfo _wormInfo;
    private Movement _movement;
    private GameManager _gameManager;
    private WeaponHolder _weaponHolder;
    
    private bool _gotInfo = false;
    private int _myTeam;
    private List<GameObject> _enemies = new List<GameObject>();
    
    private GameObject _currentTarget = null;
    private bool _enemiesAlive = false;
    
    private bool _startedMoving;

    void Awake()
    {
        _wormInfo = GetComponent<WormInfo>();
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        if (!_gotInfo) // Start() gets triggered everytime we re-activate the AI control, so make sure we only get info about ourselves once
        {
            Debug.Log("Getting info");
            
            // Get what team I'm playing for
            _myTeam = _wormInfo.GetTeam();

            // Add everyone who's not on my team to my enemy list
            foreach (List<GameObject> team in _gameManager.GetAllTeams())
            {
                foreach (GameObject worm in team)
                {
                    if (worm.GetComponent<WormInfo>().GetTeam() != _myTeam && worm.GetComponent<Health>().GetHealth() > 0)
                    {
                        _enemies.Add(worm);
                    }
                }
            }

            if (_enemies.Count > 0)
            {
                _enemiesAlive = true;
            }

            _gotInfo = true;
        }
        
        // Simulate a thinking period
        _startedMoving = false;
        StartCoroutine(WaitBeforeMoving(Random.Range(1, 3))); 
        
    }
    
    void FixedUpdate()
    {
        if (!_startedMoving)
        {
            return;
        }
        
        if (Random.Range(0, 1000) < 1)
        {
            // Switch weapons occasionally
            _weaponHolder.NextWeapon();
        }

        if (_enemiesAlive)
        {
            if (_currentTarget == null || _currentTarget.activeSelf == false) // No target or current target disabled: get a new target
            {
                _currentTarget = FindNearestEnemy();
            }
            
            // Move closer to our target if we are far away
            if (Vector3.Distance(_currentTarget.transform.position, transform.position) > 5f)
            {
                _movement.MoveTowards(_currentTarget.transform.position);
            }
            else // Otherwise, start firing
            {
                _movement.RotateTowards(_currentTarget.transform.position); // Since the bullets push the worms slightly, make sure we keep looking at them
                _weaponHolder.Fire();
            }
        }
        
    }

    GameObject FindNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float nearestEnemyDistance = 0;
        float distance;

        foreach (GameObject enemy in _enemies)
        {
            if (enemy.activeSelf) // Enemy is active (not dead)
            {
                distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (nearestEnemy == null || distance < nearestEnemyDistance)
                {
                    nearestEnemyDistance = distance;
                    nearestEnemy = enemy;
                }      
            }
        }
        
        // If we're still null here, it means there are no enemies left
        if (nearestEnemy == null)
        {
            _enemiesAlive = false;
        }

        return nearestEnemy;
    }
    
    IEnumerator WaitBeforeMoving(float delay)
    {
        yield return new WaitForSeconds(delay);
        _startedMoving = true;
    }
}