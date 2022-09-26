using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;


[RequireComponent(typeof(WormInfo))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class ControlledByAI : MonoBehaviour
{
    private WormInfo _wormInfo;
    private Movement _movement;

    private GameManager _gameManager;
    private WeaponHolder _weaponHolder;
    private PickupManager _pickupManager;
    
    private bool _gotInfo = false;
    private int _myTeam;
    private List<GameObject> _enemies = new List<GameObject>();

    private List<GameObject> _pickups;
    private float distanceToPickup;
    private float distanceToEnemy;

    private GameObject _currentPickupTarget;
    private GameObject _currentEnemyTarget = null;
    private bool _enemiesAlive = false;
    
    private bool _startedMoving;
    private bool _unstucking = false;

    private Vector3 _lastPosition;
    private RaycastHit hit;

    void Awake()
    {
        _wormInfo = GetComponent<WormInfo>();
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _gameManager = FindObjectOfType<GameManager>(); // We need this to get info about all worms
        _pickupManager = FindObjectOfType<PickupManager>(); // We need this to know about pickups
    }
    
    void Start()
    {
        // Start() gets triggered everytime we re-activate the AI control, so make sure we only get info about ourselves once
        // (Cant do it in awake because worms haven't been generated yet)
        if (!_gotInfo) 
        {
            // Get what team I'm playing for
            _myTeam = _wormInfo.GetTeam();

            // Go through every team...
            foreach (List<GameObject> team in _gameManager.GetAllTeams())
            {
                foreach (GameObject worm in team) // ... every worm ...
                {
                    // ... add everyone who's not on my team and has >0 HP to my enemy list
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

            _gotInfo = true; // So we don't go through this again next time we get activated
        }

        // Fake a thinking period
        _startedMoving = false;
        StartCoroutine(WaitBeforeMoving(Random.Range(1, 2))); 
        
    }

    void Update()
    {
        if (!_startedMoving || _unstucking)
        {
            // The fake thinking period is still active
            // or
            // We are in the process of getting unstuck
            // = dont do any other movement
            return; 
        }

        _currentPickupTarget = FindNearestPickup();
        _currentEnemyTarget = FindNearestEnemy();

        if (_currentPickupTarget != null)
        {
            distanceToPickup = Vector3.Distance(transform.position, _currentPickupTarget.transform.position);
        }

        if (_currentEnemyTarget != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, _currentEnemyTarget.transform.position);
        }
        
        // Any obstacles in front of us?
        if (Physics.SphereCast(transform.position, 2, transform.forward - transform.up, out RaycastHit hitinfo, 1.5f) && hitinfo.transform.CompareTag("Obstacle"))
        {
            StartCoroutine(AvoidObstacle(hitinfo.transform));
        }
        else if ((_currentPickupTarget != null) && (distanceToPickup < 5f || _weaponHolder.HasAmmoInAnyWeapon() == false))
        {
            // We're close to a pickup or we're out of ammo = go for pickup
            _movement.MoveTowards(_currentPickupTarget.transform.position);
        }
        else if (_enemiesAlive)
        {
            if (_weaponHolder.HasAmmoInThisWeapon() == false)
            {
                _weaponHolder.NextWeapon();
            }
            
            if (distanceToEnemy > 6f) // Move closer to our target if we are far away...
            {
                _movement.MoveTowards(_currentEnemyTarget.transform.position);
            }
            else // ... otherwise, start firing
            {
                if (Random.Range(0, 100) < 30)
                {
                    _movement.RotateTowards(_currentEnemyTarget.transform.position); // Since the bullets push the worms slightly, make sure we keep looking at them
                    _weaponHolder.Fire();
                }
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
            if (enemy.activeSelf && enemy.GetComponent<Health>().GetHealth() > 0) // Enemy is active and not dead
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

    GameObject FindNearestPickup()
    {
        GameObject nearestPickup = null;
        float nearestPickupDistance = 0;
        foreach (GameObject pickup in _pickupManager.GetActivePickups())
        {
            if (pickup == null || pickup.activeSelf == false)
            {
                continue; // Not active pickup, go to next
            }
            
            // Find nearest pickup
            float distance = Vector3.Distance(transform.position, pickup.transform.position);
            if (nearestPickup == null || distance < nearestPickupDistance)
            {
                nearestPickup = pickup.gameObject;
                nearestPickupDistance = distance;
            }
        }

        return nearestPickup;
    }
    
    IEnumerator WaitBeforeMoving(float delay)
    {
        yield return new WaitForSeconds(delay);
        _startedMoving = true;
    }

    IEnumerator AvoidObstacle(Transform danger)
    {
        _unstucking = true;
        
        float distanceToDanger = Vector3.Distance(transform.position, danger.position);
        
        
        Vector3 dest = transform.position + (transform.right * -distanceToDanger/2); // To the left
        Vector3 dest2 = dest + (transform.right * -distanceToDanger/2) + (transform.forward * distanceToDanger/2); // Up left
        Vector3 dest3 = dest2 + (transform.forward * distanceToDanger); // Straight ahead
        
        while (Vector3.Distance(transform.position, dest) > 1)
        {
            _movement.MoveTowards(dest);
            yield return new WaitForEndOfFrame();
        }
        
        while (Vector3.Distance(transform.position, dest2) > 1)
        {
            _movement.MoveTowards(dest2);
            yield return new WaitForEndOfFrame();
        }
        
        while (Vector3.Distance(transform.position, dest3) > 1)
        {
            _movement.MoveTowards(dest3);
            yield return new WaitForEndOfFrame();
        }

        // We should be in the clear now??
        
        _unstucking = false;
    }
}