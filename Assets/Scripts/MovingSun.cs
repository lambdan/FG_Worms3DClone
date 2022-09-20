using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingSun : MonoBehaviour
{
    [SerializeField] private List<Vector3> points;
    [SerializeField] private float _speed;

    private int _currentPoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[_currentPoint], _speed * Time.fixedDeltaTime);
        transform.LookAt(Vector3.zero);

        if (transform.position == points[_currentPoint])
        {
            _currentPoint += 1;
            if (_currentPoint >= points.Count)
            {
                _currentPoint = 0;
            }
        }
    }
}
