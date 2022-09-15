using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionAction : MonoBehaviour
{
    [SerializeField] private DamageGiver _damageGiver;
    

    private bool _givesDamage = false;

    void Awake()
    {
        if (_damageGiver != null)
        {
            _givesDamage = true;
        }
    }

    public void Action(GameObject target)
    {
        if (_givesDamage)
        {
            DamageTaker _dmgTaker = target.GetComponentInParent<DamageTaker>();
            if (_dmgTaker != null) // Target can take damage
            {
                _dmgTaker.TakeDamage(_damageGiver.GetDamageAmount());
            }
        }
    }
}