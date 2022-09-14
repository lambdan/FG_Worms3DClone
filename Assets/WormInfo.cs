using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormInfo : MonoBehaviour
{
    [SerializeField] private Transform _cameraGlue;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetCameraGlue()
    {
        return _cameraGlue.transform;
    }
    
}
