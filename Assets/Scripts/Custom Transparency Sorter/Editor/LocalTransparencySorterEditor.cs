using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalTransparencySorter))]
[CanEditMultipleObjects]
public class LocalTransparencySorterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LocalTransparencySorter sorter = target as LocalTransparencySorter;

        EditorGUI.BeginChangeCheck();

        DrawDefaultInspector();

        bool somethingChanged = EditorGUI.EndChangeCheck();

        EditorGUILayout.Separator();

        if (GUILayout.Button("Update") || somethingChanged)
            sorter.UpdateTransparency();
    }
}
