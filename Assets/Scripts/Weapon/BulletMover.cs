using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    void FixedUpdate()
    {
        transform.position += transform.forward * (_speed * Time.fixedDeltaTime);
    }
}
