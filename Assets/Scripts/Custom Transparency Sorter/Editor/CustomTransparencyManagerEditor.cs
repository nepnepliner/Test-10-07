using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomTransparencyManager))]
public class CustomTransparencyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        if (GUILayout.Button("Update All"))
            ((CustomTransparencyManager)target).UpdateAll();
    }
}
