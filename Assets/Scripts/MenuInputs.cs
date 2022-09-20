using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputs : MonoBehaviour
{
    public List<TMP_Text> menuEntries;
    public Keyboard kb;
    public Gamepad gp;

    private int _selectionIndex = 0;
    
    // Update is called once per frame
    void Update()
    {
        kb = Keyboard.current;
        gp = Gamepad.current;
        
        if (kb.anyKey.wasPressedThisFrame)
        {
            if (kb.enterKey.wasPressedThisFrame)
            {
                Select();
            }
            
            if (kb.upArrowKey.wasPressedThisFrame)
            {
                MoveUp();
            }

            if (kb.downArrowKey.wasPressedThisFrame)
            {
                MoveDown();
            }
        }

        if (gp.wasUpdatedThisFrame)
        {
            if (gp.buttonSouth.wasPressedThisFrame)
            {
                Select();
            }

            if (gp.dpad.down.wasPressedThisFrame)
            {
                MoveDown();
            }

            if (gp.dpad.up.wasPressedThisFrame)
            {
                MoveUp();
            }
            
        }


    }
    
    public virtual void MakeActive(int entryIndex)
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

    public int GetSelectionIndex()
    {
        return _selectionIndex;
    }
    
    public virtual void newSelection(int selection)
    {
        _selectionIndex = selection;
        MakeActive(_selectionIndex);
    }
    
    public virtual void Select(){} // Each menu is gonna have different ways to deal with selection

    public virtual void MoveUp()
    {
        int prev = _selectionIndex - 1;
        if (prev < 0)
        {
            prev = menuEntries.Count - 1; // At top = move to bottom
        }
        newSelection(prev);
    }

    public virtual void MoveDown()
    {
        int next = _selectionIndex + 1;
        if (next >= menuEntries.Count)
        {
            next = 0;
        }

        newSelection(next);
    }
}
