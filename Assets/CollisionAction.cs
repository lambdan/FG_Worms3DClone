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
            // Check if target has a damage taker, and if so, give it the damage
            DamageTaker _dmgTaker = target.GetComponentInParent<DamageTaker>();
            if (_dmgTaker != null)
            {
                _dmgTaker.TakeDamage(_damageGiver.GetDamageAmount());
            }
        }
    }
}