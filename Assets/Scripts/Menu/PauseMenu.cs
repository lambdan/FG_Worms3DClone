using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MenuInputs
{
    [SerializeField] private Canvas _canvas;

    public override void Select()
    {
        switch (GetSelectionIndex())
            {
                case 0: // Resume game
                    FindObjectOfType<GameManager>().TogglePause();
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
