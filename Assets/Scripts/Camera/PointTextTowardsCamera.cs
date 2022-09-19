using UnityEngine;

public class PointTextTowardsCamera : MonoBehaviour
{
    private Camera _cam;
    
    // Start is called before the first frame update
    void Awake()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion q = Quaternion.LookRotation(transform.position - _cam.transform.position);
        transform.rotation = q;

    }
}
