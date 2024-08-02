using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUIDUtilities
{
    public static string GUID<T>(this T obj) where T : MonoBehaviour
    {
        UniqueIdContainer uniqueIdContainer = obj.GetComponent<UniqueIdContainer>();
        if (uniqueIdContainer == null)
            return "";
        return obj.name + "_" + uniqueIdContainer.UniqueId.ToString() + "_" + typeof(T).ToString();
    }
}

