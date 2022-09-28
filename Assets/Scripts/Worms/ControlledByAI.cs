using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    private GameManagerV2 _gameManager;
    private WeaponHolder _weaponHolder;
    private PickupManager _pickupManager;
    
    private Team _myTeam;
    private List<Worm> _aliveEnemies;
    private Worm _currentEnemy;

    private List<GameObject> _pickups;
    private float distanceToPickup;
    private float distanceToEnemy;

    private GameObject _currentPickupTarget;

    private bool _startedMoving;
    private bool _unstucking;

    private Vector3 _lastPosition;
    private RaycastHit hit;

    void Awake()
    {
        _wormInfo = GetComponent<WormInfo>();
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
    }
    
    void Start()
    {
        // This gets run everytime AI script gets enabled
        _currentEnemy = GetNearestEnemy();
        
        // Fake a thinking period
        _startedMoving = false;
        StartCoroutine(WaitBeforeMoving(Random.Range(0, 1))); 
        
    }
    
    void Update()
    {
        
        if (!_startedMoving || _unstucking)
        {
            return; 
        }
        
        distanceToEnemy = Vector3.Distance(transform.position, _currentEnemy.GetTransform().position);
        _currentPickupTarget = FindNearestPickup();
        distanceToPickup = Vector3.Distance(transform.position, _currentPickupTarget.transform.position);
        
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
        else if (_aliveEnemies.Count > 0)
        {
            if (_weaponHolder.HasAmmoInThisWeapon() == false)
            {
                _weaponHolder.NextWeapon();
            }
            
            if (distanceToEnemy > 6f) // Move closer to our target if we are far away...
            {
                _movement.MoveTowards(_currentEnemy.GetTransform().position);
            }
            else // ... otherwise, start firing
            {
                if (Random.Range(0, 100) < 30)
                {
                    _movement.RotateTowards(_currentEnemy.GetTransform().position); // Since the bullets push the worms slightly, make sure we keep looking at them
                    _weaponHolder.Fire();
                }
            }            
        }

        if (_currentEnemy.IsAlive() == false)
        {
            _currentEnemy = GetNearestEnemy();
        }
    }

    Worm GetNearestEnemy()
    {
        _aliveEnemies = _gameManager.GetAliveEnemiesOfTeam(_myTeam);
        return _aliveEnemies[Random.Range(0, _aliveEnemies.Count)];
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

        var position = transform.position;
        float distanceToDanger = Vector3.Distance(position, danger.position);
        
        
        Vector3 dest = position + (transform.right * -distanceToDanger/2); // To the left
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

    public void SetGameManager(GameManagerV2 gameManager)
    {
        _gameManager = gameManager;
    }

    public void SetPickupManager(PickupManager pickupManager)
    {
        _pickupManager = pickupManager;
    }

    public void SetTeam(Team newTeam)
    {
        _myTeam = newTeam;
    }
}