using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReceiver : MonoBehaviour
{
    public void ReceiveItem(ItemProperties ip)
    {
        Debug.Log(this.gameObject.name + " received + " + ip.name);

        if (ip.healAmount != 0)
        {
            GetComponent<Health>().ChangeHealth(ip.healAmount);
        }

        if (ip.maxHPIncrease != 0)
        {
            GetComponent<Health>().ChangeMaxHealth(ip.maxHPIncrease);
        }

        if (ip.invincibilityTime > 0)
        {
            GetComponent<Health>().StartInvincibility(ip.invincibilityTime);
        }
    }
}