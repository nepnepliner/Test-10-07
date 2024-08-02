using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PoseManager))]
public class PoseManagerEditor : Editor
{
    private PoseManager _poseManager;

    private bool _creationFoldout = false;
    private string _createPath = "Assets/Poses";
    private string _createName = "Pose";

    private void OnEnable()
    {
        _poseManager = (PoseManager)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
            _poseManager.SavePose();
        if (GUILayout.Button("Load"))
            _poseManager.LoadPose();
        GUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        _creationFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_creationFoldout, "Create New Pose");
        if (_creationFoldout)
        {
        _createPath = EditorGUILayout.TextField("Path: ", _createPath);
        _createName = EditorGUILayout.TextField("Name: ", _createName);
        if (GUILayout.Button("Create"))
            _poseManager.CreatePose(_createPath, _createName);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
