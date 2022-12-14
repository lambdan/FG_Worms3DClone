using UnityEngine;

public class CollisionAction : MonoBehaviour
{
    [SerializeField] private DamageGiver _damageGiver;
    [SerializeField] private PickupSO _pickup;
    
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
            DamageTaker _damageTaker = target.GetComponentInParent<DamageTaker>();
            if (_damageTaker != null)
            {
                _damageTaker.TakeDamage(_damageGiver.GetDamageAmount());
            }
        }
        
        if (_pickup != null)
        {
            _pickup.OnPickup(target);
            
            if (_pickup.pickupSound)
            {
                AudioSource.PlayClipAtPoint(_pickup.pickupSound, transform.position);
            }
            
            gameObject.SetActive(false);
        }
        
        
    }

    public PickupSO GetPickupScript()
    {
        return _pickup;
    }
}