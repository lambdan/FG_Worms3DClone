using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform _target;

    private Vector3 _offset;
    
    void Start()
    {
        _offset = (transform.position - _target.transform.position);
    }
    
    void LateUpdate()
    {
        transform.position = _target.transform.position + _offset;
    }
}
