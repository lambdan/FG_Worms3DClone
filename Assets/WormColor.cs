using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public void SetNewColor(Color c)
    {
        _meshRenderer.material.color = c;
    }
}
