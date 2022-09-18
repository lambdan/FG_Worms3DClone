using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WormInfo))]
public class WormState : MonoBehaviour
{
    private InputListener _inputListener;
    private ControlledByAI _controlledByAI;
    private WormInfo _wormInfo;
    [SerializeField] private HealthBar _healthBar;
    
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
        _healthBar.StartPulsing();

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
        _healthBar.StopPulsing();
        _controlledByAI.enabled = false;
        _inputListener.enabled = false;
    }
}
