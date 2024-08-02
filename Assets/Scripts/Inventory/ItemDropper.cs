using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private Vector2 _dropPivot;
    [SerializeField] private float _scatter;

    private Vector2 WorldDropPivot => transform.TransformPoint(_dropPivot);

    public void Drop(Item item, int count)
    {
        if (item == null)
            return;

        ItemContainer container = Instantiate(DropManager.Instance.ContainerPrefab, RandomDropPosition, Quaternion.identity);
        container.TryAdd(item, count);
    }

    public void Drop(ItemContainer itemContanier)
    {
        if (itemContanier == null)
            return;

        Instantiate(itemContanier, RandomDropPosition, Quaternion.identity);
    }

    private Vector2 RandomDropPosition => WorldDropPivot + Random.insideUnitCircle * _scatter * 2;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(WorldDropPivot, _scatter * 2);
    }
}
