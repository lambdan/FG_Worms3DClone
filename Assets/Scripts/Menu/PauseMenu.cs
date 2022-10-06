using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private Canvas _canvas;

    public void ResumeGame()
    {
        _gameManager.TogglePause();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Scenes/PlayScene");
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
    
    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
