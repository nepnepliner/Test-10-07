using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PositionTransparencySorter))]
[CanEditMultipleObjects]
public class PositionTransparencySorterEditor : Editor
{
    private bool _editPivot;

    public override void OnInspectorGUI()
    {
        PositionTransparencySorter sorter = target as PositionTransparencySorter;

        EditorGUI.BeginChangeCheck();

        DrawDefaultInspector();

        bool somethingChanged = EditorGUI.EndChangeCheck();

        EditorGUILayout.Separator();

        _editPivot = GUILayout.Toggle(_editPivot, "EditPivot");

        if (GUILayout.Button("Update") || somethingChanged)
            sorter.UpdateTransparency();
    }

    protected virtual void OnSceneGUI()
    {
        if (_editPivot)
            EditPivotGui();
    }

    private void EditPivotGui()
    {
        PositionTransparencySorter sorter = target as PositionTransparencySorter;
        EditorGUI.BeginChangeCheck();
        Vector3 worldPivot = sorter.WorldPivot;
        Vector3 newWorldPivot = Handles.Slider2D(1, worldPivot, Vector3.back, Vector3.right, Vector3.up, HandleUtility.GetHandleSize(worldPivot) * 0.1f, Handles.DotHandleCap, Vector2.one * 0.1f);
        if (EditorGUI.EndChangeCheck())
            sorter.WorldPivot = newWorldPivot;
    }
}
