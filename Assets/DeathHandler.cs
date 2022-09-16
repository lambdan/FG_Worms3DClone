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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Died()
    {
        Debug.Log("Death Handler: reporting death of " + this.name + " to Game Manager");
        _gameManager.ReportDeath(this.gameObject, _wormInfo.GetTeam());
        _deathAnim.TriggerDeathAnimation();
    }
}
