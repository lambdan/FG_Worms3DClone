using UnityEngine;

public class PointTextTowardsCamera : MonoBehaviour
{
    private Camera _cam;

    void Awake()
    {
        _cam = Camera.main;
    }
    
    void LateUpdate()
    {
        // transform.LookAt doesnt work here... makes it mirrored
        Quaternion q = Quaternion.LookRotation(transform.position - _cam.transform.position);
        transform.rotation = q;
    }
}
