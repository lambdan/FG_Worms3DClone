using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PatrolRoute : MonoBehaviour
{
    [SerializeField] private List<Vector3> _points;
    private Movement _movement;

    private int _currentPoint = 0;
    private Vector3 _currentDestination;

    void Awake()
    {
        _movement = GetComponent<Movement>();
    }
    
    void Update()
    {
        _movement.MoveTowards(_currentDestination);
        if (Vector3.Distance(transform.position, _points[_currentPoint]) < 4f)
        {
            SetCurrentPoint(GetNextPoint(_currentPoint));
        }
    }
    
    public List<Vector3> GetPatrolPoints()
    {
        return _points;
    }

    public void SetCurrentPoint(int p)
    {
        _currentPoint = p;
        _currentDestination = new Vector3(_points[_currentPoint].x, 0, _points[_currentPoint].z);
    }
    
    int GetNextPoint(int current)
    {
        int next = current + 1;
        if (next >= _points.Count)
        {
            next = 0;
        }

        return next;
    }
    
}
