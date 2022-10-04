using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuSystem : MonoBehaviour
{
    public AudioClip selectionSound;
    protected AudioSource _audioSource;
    public List<TMP_Text> menuEntries;
    private int _selectionIndex = 0;
    [HideInInspector] public UnityEvent menuSelection;
    [HideInInspector] public UnityEvent menuIncrease;
    [HideInInspector] public UnityEvent menuDecrease;
    
    void MakeActive(int entryIndex)
    {
        // Make the current entry yellow and the rest white
        for (int i = 0; i < menuEntries.Count; i++)
        {
            if (i == entryIndex)
            {
                menuEntries[i].color = Color.yellow;
            }
            else
            {
                menuEntries[i].color = Color.white;
            }
            
        }
    }

    protected int GetSelectionIndex()
    {
        return _selectionIndex;
    }

    protected void newSelection(int selection)
    {
        _selectionIndex = selection;
        MakeActive(_selectionIndex);
        _audioSource.Play();
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        _audioSource.Play();
        menuSelection.Invoke();
    }

    public void Increase(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        _audioSource.Play();
        menuIncrease.Invoke();
    }
    
    public void Decrease(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        _audioSource.Play();
        menuDecrease.Invoke();
    }

    public void MoveUp(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        int prev = _selectionIndex - 1;
        if (prev < 0)
        {
            prev = menuEntries.Count - 1; // At top = move to bottom
        }
        newSelection(prev);
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        int next = _selectionIndex + 1;
        if (next >= menuEntries.Count)
        {
            next = 0;
        }

        newSelection(next);
    }
}
