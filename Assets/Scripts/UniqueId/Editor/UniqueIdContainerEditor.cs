using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UniqueIdContainer))]
public class UniqueIdContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.LabelField((target as UniqueIdContainer).UniqueId.ToString());
    }
}
