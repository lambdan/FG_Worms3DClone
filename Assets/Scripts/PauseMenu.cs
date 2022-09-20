using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MenuInputs
{
    private bool _pauseMenuActive = false;
    [SerializeField] private Canvas _canvas;

    public override void Select()
    {
        if (_pauseMenuActive)
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
    }

    void Awake()
    {
        newSelection(0); // To make the first item be hovered
    }

    public void Activate()
    {
        _canvas.gameObject.SetActive(true);
        _pauseMenuActive = true;
    }

    public void Deactivate()
    {
        _canvas.gameObject.SetActive(false);
        _pauseMenuActive = false;
    }
}
