using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(WeaponHolder))]
public class ControlledByAI : MonoBehaviour
{
    private Movement _movement;

    private GameManager _gameManager;
    private WeaponHolder _weaponHolder;
    private PickupManager _pickupManager;
    
    private Team _myTeam;
    private List<Worm> _aliveEnemies;
    private Worm _currentEnemy;

    private List<GameObject> _pickups;
    private float distanceToPickup;
    private float distanceToEnemy;

    private GameObject _currentPickupTarget;
    private bool _avoidingObstacle;

    private Vector3 _lastPosition;
    private RaycastHit hit;

    void Awake()
    {
        _movement = GetComponent<Movement>();
        _weaponHolder = GetComponent<WeaponHolder>();
    }
    
    void Update()
    {
        if (_avoidingObstacle)
        {
            return; 
        }
        
        if (_currentEnemy.IsAlive() == false)
        {
            _currentEnemy = GetNearestEnemy();
        }
        distanceToEnemy = Vector3.Distance(transform.position, _currentEnemy.GetTransform().position);
        
        if (_currentPickupTarget != null && _currentPickupTarget.activeSelf)
        {
            distanceToPickup = Vector3.Distance(transform.position, _currentPickupTarget.transform.position);
        }
        else
        {
            _currentPickupTarget = FindNearestPickup();
        }
        
        // Any obstacles in front of us?
        if (Physics.SphereCast(transform.position, 2, transform.forward - transform.up, out RaycastHit hitinfo, 1.5f) && hitinfo.transform.CompareTag("Obstacle"))
        {
            StartCoroutine(SimpleAvoidObstacle());
        }
        else if (_currentPickupTarget != null && _weaponHolder.HasAmmoInAnyWeapon() == false)
        {
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
                _movement.AxisInput(Vector2.zero); // To stop moving
                _movement.RotateTowards(_currentEnemy.GetTransform().position); // Since the bullets push the worms slightly, make sure we keep looking at them
                _weaponHolder.Fire();
            }            
        }
    }

    Worm GetNearestEnemy()
    {
        _aliveEnemies = _gameManager.GetAliveEnemiesOfTeam(_myTeam);
        Worm nearestEnemy = null;
        foreach (Worm enemy in _aliveEnemies)
        {
            if (nearestEnemy == null || DistanceTo(enemy.GetTransform()) < DistanceTo(nearestEnemy.GetTransform()))
            {
                nearestEnemy = enemy;
            } 
        }

        return nearestEnemy;
    }

    float DistanceTo(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
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

    IEnumerator SimpleAvoidObstacle()
    {
        _avoidingObstacle = true;
        
        // Turn around
        Vector3 destination  = transform.position - (transform.forward * 3) - (transform.right * 20);
        float timeStarted = Time.time;
        while (Time.time - timeStarted < 2)
        {
            _movement.MoveTowards(destination);
            yield return new WaitForFixedUpdate();
        }
        _avoidingObstacle = false;
    }

    public void SetGameManager(GameManager gameManager)
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

    void OnEnable()
    {
        _currentEnemy = GetNearestEnemy();
        _currentPickupTarget = FindNearestPickup();
        
        // Fake a thinking period
    }

    void OnDisable()
    {
        StopAllCoroutines();
        _movement.AxisInput(Vector2.zero);
    }
}