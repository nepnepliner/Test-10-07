using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class UniqueIdInitializer
{
    /*static UniqueIdInitializer()
    {
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private static void OnHierarchyChanged()
    {
        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.GetComponent<UniqueIdContainer>() == null)
            {
                go.AddComponent<UniqueIdContainer>();
            }
        }
    }*/
}
