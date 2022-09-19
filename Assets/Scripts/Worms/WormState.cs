using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WormInfo))]
public class WormState : MonoBehaviour
{
    private InputListener _inputListener;
    private ControlledByAI _controlledByAI;
    private WormInfo _wormInfo;

    public UnityEvent activated;
    public UnityEvent deactivated;

    private bool _active = false;
    
    void Awake()
    {
        _inputListener = GetComponent<InputListener>();
        _wormInfo = GetComponent<WormInfo>();
        _controlledByAI = GetComponent<ControlledByAI>();
    }
    
    public bool IsActive()
    {
        return _active;
    }
    
    public void Activate()
    {
        _active = true;

        if (_wormInfo.IsAIControlled())
        {
            _controlledByAI.enabled = true;
            _inputListener.enabled = false;
        }
        else
        {
            _controlledByAI.enabled = false;
            _inputListener.enabled = true;  
        }
        
    }
    
    public void Deactivate()
    {
        _active = false;
        _controlledByAI.enabled = false;
        _inputListener.enabled = false;
    }
}
