using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WormInfo))]
public class WormState : MonoBehaviour
{
    private InputReceiver _inputReceiver;
    private ControlledByAI _controlledByAI;
    private WormInfo _wormInfo;
    [SerializeField] private HealthBar _healthBar;
    
    private bool _active = false;
    
    void Awake()
    {
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
        _active = true;
        _healthBar.StartPulsing();

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
        
    }
    
    public void Deactivate()
    {
        _active = false;
        _healthBar.StopPulsing();
        _controlledByAI.enabled = false;
        _inputReceiver.enabled = false;
    }
}
