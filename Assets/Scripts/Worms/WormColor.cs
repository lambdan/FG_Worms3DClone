using UnityEngine;

public class WormColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private Color _currentColor;
    
    public void SetNewColor(Color c)
    {
        _meshRenderer.material.color = c;
        _currentColor = c;
    }

    public Color GetColor()
    {
        return _currentColor;
    }
}
