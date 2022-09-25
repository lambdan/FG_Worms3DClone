using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
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
        _gameManager = FindObjectOfType<GameManager>();
        _pickupManager = FindObjectOfType<PickupManager>();
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
        if (!_startedMoving)
        {
            return; // The fake thinking period is still active: do nothing
        }

        if (_unstucking)
        {
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

        //Debug.Log("distance to pickup: " + distanceToPickup);
        //Debug.Log("distance to enemy: " + distanceToEnemy);
        
        
        // Anything in front of us?
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5) && hit.transform.CompareTag("Obstacle"))
        {
            Debug.Log("hittin " + hit.transform.tag + "," + hit.transform.name);
            StartCoroutine(Unstucking());
        }
        else if ((_currentPickupTarget != null) && (distanceToPickup < 10f || _weaponHolder.HasAmmoInAnyWeapon() == false))
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

    IEnumerator Unstucking()
    {
        Debug.Log("Start unstuck");
        _unstucking = true;
        Vector3 destination = transform.position + (transform.right * -10);

        while (Vector3.Distance(transform.position, destination) > 3)
        {
            _movement.MoveTowards(destination);
            yield return new WaitForFixedUpdate();
        }

        _unstucking = false;
        Debug.Log("End unstuck");
    }
}