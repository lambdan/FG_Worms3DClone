using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


[RequireComponent(typeof(Health))]
public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private bool _destroyWhenDone;
    [SerializeField] private float _speed;

    private Quaternion _startRotation;
    private Quaternion _targetRotation;
    private bool _animationStarted = false;
    private bool _animationDone = false;

    void FixedUpdate()
    {
        // Check if animation has been triggered
        if (_animationStarted && !_animationDone)
        {
            // Rotate on the z axis to 90 degrees make the worm lie down
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.fixedDeltaTime);
            
            // Also ascend up into heaven
            transform.position += Vector3.up * _speed * Time.fixedDeltaTime;
            
            if (transform.position.y > 20)
            {
                _animationDone = true;
            }
            
        }

        // Animation is done, the entire worm can be destroyed
        if (_animationDone && _destroyWhenDone)
        {
            Destroy(this.gameObject);
        }
    }

    public void TriggerDeathAnimation()
    {
        _startRotation = transform.rotation;
        _targetRotation = Quaternion.Euler(new Vector3(0, 0, 90));

        GetComponentInChildren<Rigidbody>().useGravity = false;        
        GetComponentInChildren<CapsuleCollider>().enabled = false;
        _animationStarted = true;
    }
}
