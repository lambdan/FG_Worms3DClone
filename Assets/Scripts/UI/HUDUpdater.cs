using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class HUDUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _teamText;
    [SerializeField] private TMP_Text _turnsPlayedText;
    [SerializeField] private Slider _turnTimeSlider;
    [SerializeField] private Animation _turnTimeSliderAnimation;
    [SerializeField] private TMP_Text _turnTimeText;
    [SerializeField] private TMP_Text _ammoText;


    public void SetTeamText(string newText)
    {
        _teamText.text = newText;
    }

    public void SetTeamColor(Color newColor)
    {
        _teamText.color = newColor;
    }
    
    public void SetPlayerText(string newText)
    {
        _nameText.text = newText;
    }
    
    public void UpdateTurnsPlayed(int rounds)
    {
        _turnsPlayedText.text = "Turn: " + rounds;
    }

    public void UpdateTurnSlider(float current)
    {
        _turnTimeSlider.value = current;

        _turnTimeText.text = current.ToString("0.0");

        if (current < 0.1)
        {
            _turnTimeSliderAnimation.Stop();
        }
        else if (_turnTimeSliderAnimation.isPlaying == false)
        {
            _turnTimeSliderAnimation.Play();
        }
    }

    public void SetTurnSliderMin(float min)
    {
        _turnTimeSlider.minValue = min;
    }

    public void SetTurnSliderMax(float max)
    {
        _turnTimeSlider.maxValue = max;
    }

    public void UpdateAmmo(int bulletsInClip,  int ammunitionHeld)
    {
        // Results in something like: 18 (120)
        _ammoText.text = bulletsInClip.ToString() + " (" + ammunitionHeld.ToString() + ")";
    }

    public void HideAmmo()
    {
        _ammoText.gameObject.SetActive(false);
    }

    public void ShowAmmo()
    {
        _ammoText.gameObject.SetActive(true);
    }
}