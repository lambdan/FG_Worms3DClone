using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _menuEntries;
    
    [SerializeField] private TMP_Text _humanMenuSelector;
    [SerializeField] private TMP_Text _aiMenuSelector;

    private int _menuIndex = 0;

    void Start()
    {
        MakeActive(_menuIndex);
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
}
