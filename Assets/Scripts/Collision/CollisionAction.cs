using UnityEngine;

public class CollisionAction : MonoBehaviour
{
    [SerializeField] private DamageGiver _damageGiver;
    [SerializeField] private ItemGiver _itemGiver;
    
    private bool _givesDamage = false;
    private bool _givesItem = false;
    
    void Awake()
    {
        if (_damageGiver != null)
        {
            _givesDamage = true;
        }

        if (_itemGiver != null)
        {
            _givesItem = true;
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

        if (_givesItem)
        {
            ItemReceiver _itemReceiver = target.GetComponentInParent<ItemReceiver>();
            if (_itemReceiver != null)
            {
                _itemReceiver.ReceiveItem( _itemGiver.GetItemProps());
            }
        }
        
        
    }
}