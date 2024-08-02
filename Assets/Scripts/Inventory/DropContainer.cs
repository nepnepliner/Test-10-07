using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropContainer : MonoBehaviour
{
    //[Header("Components")]
    private ItemDropper _dropper;

    [SerializeField] private ItemContainer[] _dropVariants;

    private void Start()
    {
        _dropper = GetComponent<ItemDropper>();
    }

    public void Drop()
    {
        ItemContainer randomDrop = RandomDrop;
        _dropper.Drop(randomDrop);
    }

    private ItemContainer RandomDrop
    {
        get
        {
            if (_dropVariants.Length > 0)
                return _dropVariants[Random.Range(0, _dropVariants.Length)];
            else
                return null;
        }
    }
}
