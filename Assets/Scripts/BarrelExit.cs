using UnityEngine;

public class BarrelExit : MonoBehaviour
{
    [SerializeField] private Transform _barrelExit;

    public Transform GetBarrelExit()
    {
        return _barrelExit;
    }
}
