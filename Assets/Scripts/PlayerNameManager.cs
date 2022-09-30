using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private GameObject _inputFieldPrefab;
    [SerializeField] private GameObject _listContainer;
    private List<GameObject> _playerInputFieldGameObjects = new List<GameObject>();
    private List<TMP_InputField> _playerInputFields = new List<TMP_InputField>();
    private SettingsManager _settingsManager;

    void Awake()
    {
        if (_settingsManager == null) // Only grab the settings manager if there isn't one already going (singleton)
        {
            _settingsManager = SettingsManager.Instance;
        }
    }
    
    void CreateInputField()
    {
        GameObject inputFieldGameObject = Instantiate(_inputFieldPrefab, _listContainer.transform);
        TMP_InputField textField = inputFieldGameObject.GetComponent<TMP_InputField>();
        textField.text = "Human " + (_playerInputFieldGameObjects.Count + 1).ToString();
        int index = _playerInputFields.Count;
        _playerInputFieldGameObjects.Add(inputFieldGameObject);
        _playerInputFields.Add(textField);
        textField.onEndEdit.AddListener(s => GrabNewName(s, index)); // Doesn't work with just putting a .Count here as index
    }
    
    public void RefreshInputContainer()
    {
        while (_playerInputFieldGameObjects.Count < _settingsManager.HowManyHumans())
        {
            CreateInputField();
        }

        for (int i = 0; i < _playerInputFieldGameObjects.Count; i++)
        {
            if (i < _settingsManager.HowManyHumans())
            {
                _playerInputFields[i].text = _settingsManager.GetPlayerNames()[i];
                _playerInputFieldGameObjects[i].SetActive(true);
            }
            else
            {
                _playerInputFieldGameObjects[i].SetActive(false);
            }
        }
    }

    public void GrabNewName(string newName, int index)
    {
        _settingsManager.SetPlayerName(index, newName);
    }
}
