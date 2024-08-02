using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemContainer))]
public class ItemContainerEditor : Editor
{
    private ItemContainer _inventoryContainer;

    private void OnEnable()
    {
        _inventoryContainer = (ItemContainer)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        DrawDefaultInspector();

        bool update = EditorGUI.EndChangeCheck();

        EditorGUILayout.Separator();

        if (GUILayout.Button("Update") || update)
        {
            _inventoryContainer.ClampCount();
            _inventoryContainer.UpdateUI();
        }
    }
}
