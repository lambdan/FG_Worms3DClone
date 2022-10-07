using System.Collections.Generic;
using UnityEngine;

public static class TeamColor
{
    private static List<Color> _usedColors = new List<Color>();
    private static readonly List<Color> _predefinedColors = new List<Color>()
    {
        Color.blue,
        Color.red,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.magenta,
        Color.black,
        Color.white
    };

    public static void ClearUsedColors()
    {
        _usedColors = new List<Color>();
    }

    public static Color GetAvailableTeamColor()
    {
        Color availableColor;
        
        if (_usedColors.Count >= _predefinedColors.Count) // Out of predefined colors, give a random
        {
            availableColor = Random.ColorHSV();
            while (_usedColors.Contains(availableColor))
            {
                availableColor = Random.ColorHSV();
            }
        }
        else
        {
            availableColor = _predefinedColors[_usedColors.Count];
        }
        
        _usedColors.Add(availableColor);
        return availableColor;
    }
    
    


}