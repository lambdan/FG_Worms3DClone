using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _menuEntries;
    
    [SerializeField] private TMP_Text _humanMenuSelector;
    [SerializeField] private TMP_Text _aiMenuSelector;

    [SerializeField] private TMP_Text _messageBox;

    [SerializeField] private SettingsManager _settingsManager;
    
    private int _menuIndex = 0;

    void Start()
    {
        newSelection(0);
    }

    void MakeActive(int entryIndex)
    {
        // Make the current entry yellow and the rest white
        for (int i = 0; i < _menuEntries.Count; i++)
        {
            if (i == entryIndex)
            {
                _menuEntries[i].color = Color.yellow;
            }
            else
            {
                _menuEntries[i].color = Color.white;
            }
            
        }
    }

    public void newSelection(int selection)
    {
        _menuIndex = selection;
        MakeActive(_menuIndex);
    }
    public void MoveUp()
    {
        int prev = _menuIndex - 1;
        if (prev < 0)
        {
            prev = _menuEntries.Count - 1; // At top = move to bottom
        }
        newSelection(prev);
    }

    public void MoveDown()
    {
        int next = _menuIndex + 1;
        if (next >= _menuEntries.Count)
        {
            next = 0;
        }

        newSelection(next);
    }

    public void Select()
    {
        //Debug.Log(_menuIndex + " was selected");

        if (_menuIndex == 0) // Start game
        {
            if (_settingsManager.GetTotalPlayers() > _settingsManager.GetMaxPlayers())
            {
                ErrorMessage(
                    "There can only be a total of " + _settingsManager.GetMaxPlayers().ToString() + " players.");
                return;
            } else if (_settingsManager.GetTotalPlayers() < 2)
            {
                ErrorMessage("There needs to be atleast 2 players");
                return;
            }
            
            SceneManager.LoadScene("Scenes/PlayScene");
        }
        
        if (_menuIndex == 1) // Humans
        {
            _settingsManager.IncrementHumans();
        } 
        else if (_menuIndex == 2) // AI's
        {
            _settingsManager.IncrementAIs();
        }
        
        RefreshPlayerCounts();
    }

    void RefreshPlayerCounts()
    {
        _humanMenuSelector.text = _settingsManager.GetHumans().ToString();
        _aiMenuSelector.text = _settingsManager.GetAIs().ToString();
    }

    void ErrorMessage(string msg)
    {
        _messageBox.color = Color.red;
        _messageBox.text = msg;
        _messageBox.gameObject.SetActive(true);
        StartCoroutine(HideMessage(5));
    }

    IEnumerator HideMessage(float duration)
    {
        float disappear = Time.time + duration;
        while (Time.time < disappear)
        {
            yield return new WaitForSeconds(1);
        }

        _messageBox.gameObject.SetActive(false);
    }
}
