using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private GameObject _inputFieldPrefab;

    private List<GameObject> _playerInputFieldGameObjects = new List<GameObject>();
    private List<TMP_InputField> _playerInputFields = new List<TMP_InputField>();

    private GameObject _container;

    private SettingsManager _settingsManager;

    private int _playerAmount;
    private int _maxPlayers;

    void Awake()
    {
        if (_settingsManager == null) // Only grab the settings manager if there isn't one already going (singleton)
        {
            _settingsManager = FindObjectOfType<SettingsManager>();
        }

        _maxPlayers = _settingsManager.GetMaxPlayers();

        // Initialize list of names & input fields with length of max players
        for (int i = 1; i <= _maxPlayers; i++)
        {

            GameObject textFieldGO = Instantiate(_inputFieldPrefab);
            TMP_InputField textField = textFieldGO.GetComponent<TMP_InputField>();

            textField.text = "Player " + i.ToString();

            _playerInputFieldGameObjects.Add(textFieldGO);
            _playerInputFields.Add(textField);
        }
    }

    public void SetContainer(GameObject target)
    {
        _container = target;
        foreach (GameObject inputfieldGO in _playerInputFieldGameObjects)
        {
            inputfieldGO.transform.SetParent(_container.transform);
        }
    }

    void UpdatePlayerAmount()
    {
        _playerAmount = _settingsManager.GetHumans();
        while (_playerInputFieldGameObjects.Count < _settingsManager.GetMaxPlayers())
        {
            GameObject textFieldGO = Instantiate(_inputFieldPrefab);
            TMP_InputField textField = textFieldGO.GetComponent<TMP_InputField>();

            textField.text = "Human " + _playerInputFieldGameObjects.Count.ToString();

            _playerInputFieldGameObjects.Add(textFieldGO);
            _playerInputFields.Add(textField);
        }
    }

    public void RefreshInputContainer()
    {
        UpdatePlayerAmount();

        for (int i = 0; i < _maxPlayers; i++)
        {
            if (i < _playerAmount)
            {
                _playerInputFieldGameObjects[i].SetActive(true);
            }
            else
            {
                _playerInputFieldGameObjects[i].SetActive(false);
            }

        }

        //Debug.Log("hello from refresh in player name manager, there are " + _playerAmount + " players");
    }

    public List<string> GetAllNames()
    {
        List<string> names = new List<string>();
        foreach (TMP_InputField inputfield in _playerInputFields)
        {
            names.Add(inputfield.text);
        }

        return names;
    }

    public void SetNames(List<string> newNames)
    {
        for (int i = 0; i < _settingsManager.GetHumans(); i++)
        {
            _playerInputFields[i].text = newNames[i];
        }
    }
}
