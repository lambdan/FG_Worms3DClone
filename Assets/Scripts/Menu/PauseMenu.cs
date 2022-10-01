using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MenuSystem
{
    private GameManager _gameManager;
    [SerializeField] private Canvas _canvas;

    void Selection()
    {
        switch (GetSelectionIndex())
            {
                case 0: // Resume game
                    _gameManager.TogglePause();
                    break;
                case 1: // Restart
                    SceneManager.LoadScene("Scenes/PlayScene");
                    break;
                case 2: // Quit to menu
                    SceneManager.LoadScene("Scenes/Menu");
                    break;
            }
    }

    void Awake()
    {
        newSelection(0); // To make the first item be hovered
        menuSelection.AddListener(Selection);
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
