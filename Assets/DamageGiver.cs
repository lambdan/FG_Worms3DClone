using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    [SerializeField] private int _damageAmount;
    
    public int GetDamageAmount()
    {
        return _damageAmount;
    }
}
