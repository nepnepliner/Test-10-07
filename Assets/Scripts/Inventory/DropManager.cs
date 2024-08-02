using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : Singleton<DropManager>
{
    [SerializeField] private ItemContainer _containerPrefab;

    public ItemContainer ContainerPrefab => _containerPrefab;
}
