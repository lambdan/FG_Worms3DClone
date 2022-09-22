using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    [SerializeField] private ItemProperties _itemProps;

    public ItemProperties GetItemProps()
    {
        return _itemProps;
    }
    
}
