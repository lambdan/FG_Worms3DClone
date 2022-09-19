using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private GameManager _gameManager;
    private DeathAnimation _deathAnim;
    private WormInfo _wormInfo;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        
        _deathAnim = GetComponent<DeathAnimation>();
        _wormInfo = GetComponent<WormInfo>();
    }
    
    public void Died()
    {
        if (_deathAnim != null)
        {
            _deathAnim.TriggerDeathAnimation();
        }

        if (_gameManager != null)
        {
            _gameManager.ReportDeath(_wormInfo.GetTeam());
        }
    }
}
