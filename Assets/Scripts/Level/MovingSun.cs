using System.Collections.Generic;
using UnityEngine;

public class MovingSun : MonoBehaviour
{
    [SerializeField] private List<Vector3> points;
    [SerializeField] private float _speed;

    private int _currentPoint = 0;

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[_currentPoint], _speed * Time.fixedDeltaTime);
        transform.LookAt(Vector3.zero); // Aim towards the center 

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
