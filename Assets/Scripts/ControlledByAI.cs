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

    private int _myTeam;
    private List<GameObject> _enemies = new List<GameObject>();
    private bool _startedMoving;
    
    private Transform _nearestEnemy;

    private float _startedTime;
    private Vector3 _startPosition;
    private bool _unstucking;

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
                if (worm.GetComponent<WormInfo>().GetTeam() != _myTeam)
                {
                    _enemies.Add(worm);
                }
            }
        }

        _startedMoving = false;
        StartCoroutine(WaitBeforeMoving(Random.Range(1, 3))); // Simulate a thinking period
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        if (_nearestEnemy != null)
        {
            Gizmos.DrawLine(transform.position, _nearestEnemy.position);
        }
        
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
            float nearestEnemyDistance = 9999;
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

            _nearestEnemy = nearestEnemy.transform;

            //Debug.Log(this.name + " nearest enemy: " + nearestEnemy.name + " (" + nearestEnemyDistance + " away)");


            // If we havent started shooting and we are far away from the enemy move towards it
            if (nearestEnemyDistance > 7f)
            {
                if (!_unstucking)
                {
                    _movement.MoveTowards(nearestEnemy.transform.position);
                    if (Vector3.Distance(_startPosition, transform.position) < 2f && Time.time - _startedTime > 1.5f)
                    {
                        // We don't seem to be moving, try going to the left for a while
                        StartCoroutine(Unstucker());
                    }
                }
                


                
            }
            else // Otherwise, start firing
            {
                _movement.RotateTowards(nearestEnemy.transform.position);

                // Simulate random presses of the trigger
                if (Random.Range(0, 100) < 10)
                {
                    _weaponHolder.Fire();
                }
            }
        }

        if (Random.Range(0, 100) < 5)
        {
            // Switch weapons occasionally
            _weaponHolder.NextWeapon();
        }
    }

    IEnumerator WaitBeforeMoving(float delay)
    {
        yield return new WaitForSeconds(delay);
        _startedMoving = true;
        _startPosition = transform.position;
        _startedTime = Time.time;
    }

    IEnumerator Unstucker()
    {
        _unstucking = true;
        var started = Time.time;
        while (Time.time - started < 1)
        {
            Debug.Log("UNSTUCKING!!!");
            _movement.MoveTowards(transform.position + (Vector3.right * 10));
            yield return new WaitForFixedUpdate();
        }

        _unstucking = false;

    }
}