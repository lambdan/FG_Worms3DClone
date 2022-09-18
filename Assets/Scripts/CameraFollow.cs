using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    private bool _active = false;

    private Vector3 _cameraDestination;

    void LateUpdate()
    {
        if (_active && _target  != null)
        {
            _cameraDestination = _target.position - (_target.transform.forward * 7) + (_target.transform.up * 3);
        }
        
        transform.position = Vector3.Lerp(transform.position, _cameraDestination, Mathf.Max(Vector3.Distance(transform.position, _cameraDestination), 10f) * Time.deltaTime * 0.1f);
        transform.LookAt(_target);
    }
    

    public void SetNewTarget(GameObject go)
    {
        _target = go.transform;
    }
    
    public void Activate()
    {
        _active = true;
    }

    public void Deactivate()
    {
        _active = false;
        _cameraDestination = _target.position + (transform.right*10) + (transform.forward*10) + (transform.up*10);
    }

    public bool GetStats()
    {
        return _active;
    }

    public Transform GetTarget()
    {
        return _target;
    }

    IEnumerator SmoothMoveToNewTarget()
    {
        while (transform.position != _cameraDestination)
        {
            transform.position = Vector3.Lerp(transform.position, _cameraDestination, 2 * Time.fixedDeltaTime);
            transform.LookAt(_target);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Debug.Log("MoveToNewTarget() done");
    }
    
    
}
