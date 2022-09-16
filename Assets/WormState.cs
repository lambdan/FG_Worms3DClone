using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WormInfo))]
public class WormState : MonoBehaviour
{
    [SerializeField] private GameObject _activeIndicator;

    private MeshRenderer _activeIndicatorMeshRender;
    private Animation _activeIndicatorAnimation;
    private InputReceiver _inputReceiver;
    private ControlledByAI _controlledByAI;
    private WormInfo _wormInfo;
    
    private bool _active = false;
    
    void Awake()
    {
        _activeIndicatorMeshRender = _activeIndicator.GetComponent<MeshRenderer>();
        _activeIndicatorAnimation = _activeIndicator.GetComponent<Animation>();
        _inputReceiver = GetComponent<InputReceiver>();
        _wormInfo = GetComponent<WormInfo>();
        _controlledByAI = GetComponent<ControlledByAI>();
    }
    
    public bool IsActive()
    {
        return _active;
    }
    
    public void Activate()
    {
        Debug.Log("Activating worm " + name);
        _active = true;

        if (_wormInfo.IsAIControlled())
        {
            _controlledByAI.enabled = true;
            _inputReceiver.enabled = false;
        }
        else
        {
            _controlledByAI.enabled = false;
            _inputReceiver.enabled = true;  
        }
        
        _activeIndicatorAnimation.enabled = true;
        _activeIndicatorMeshRender.enabled = true;
    }
    
    public void Deactivate()
    {
        Debug.Log("Deactivating worm " + name);
        _active = false;
        

        _controlledByAI.enabled = false;
        _inputReceiver.enabled = false;
        _activeIndicatorAnimation.enabled = false;
        _activeIndicatorMeshRender.enabled = false;
    }
}
