using UnityEngine;

[RequireComponent(typeof(WormInfo))]
public class DeathHandler : MonoBehaviour
{
    private GameManager _gameManager;
    private WormInfo _wormInfo;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _wormInfo = GetComponent<WormInfo>();
    }
    
    public void Died()
    {
        if (_gameManager != null)
        {
            // Report death in this team
            _gameManager.ReportDeath(_wormInfo.GetTeam());
        }
    }
}
