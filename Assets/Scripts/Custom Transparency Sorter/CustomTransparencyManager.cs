using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTransparencyManager : Singleton<CustomTransparencyManager>
{
    public void UpdateAll()
    {
        List<CustomTransparencySorter> sorters = new List<CustomTransparencySorter>();
        sorters.AddRange(FindObjectsOfType<LocalTransparencySorter>());
        sorters.AddRange(FindObjectsOfType<PositionTransparencySorter>());
        foreach (CustomTransparencySorter sorter in sorters)
            sorter.UpdateTransparency();
    }
}
