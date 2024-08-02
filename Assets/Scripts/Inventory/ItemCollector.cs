using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private Inventory _inventory;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemContainer container = other.GetComponent<ItemContainer>();
        if (container != null && container.IsBlocked == false)
            CollectItem(container);
    }

    private void CollectItem(ItemContainer container)
    {
        Debug.Log(container);
        container.TryRemove(container.Item, _inventory.TryAdd(container.Item, container.Count));
    }
}

