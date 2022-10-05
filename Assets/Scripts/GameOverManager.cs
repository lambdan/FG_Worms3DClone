using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private float _timeUntilBackToMenu = 5f;
    [SerializeField] private TMP_Text _winningTeamText;
    [SerializeField] private TMP_Text _pointsText;
    [SerializeField] private TMP_Text _timerText;

    public void SetWinningTeam(string winningTeam)
    {
        _winningTeamText.text = winningTeam;
    }

    public void SetPoints(int points)
    {
        _pointsText.text = $"{points.ToString()} points";
    }

    public void UpdateCountdown(float timeRemaining)
    {
        _timerText.text = $"Going back to main menu in {timeRemaining.ToString("0.0")}...";
    }
    
    void Start()
    {
        SetWinningTeam(PlayerPrefs.GetString("LastWinner"));
        SetPoints(PlayerPrefs.GetInt("LastWinningScore"));
        StartCoroutine(CountdownToMenu());
    }

    IEnumerator CountdownToMenu()
    {
        float timeFinished = Time.time + _timeUntilBackToMenu;
        while (Time.time < timeFinished)
        {
            UpdateCountdown(timeFinished - Time.time);
            yield return new WaitForFixedUpdate();
        }
        SceneManager.LoadScene("Scenes/Menu");
    }
}
