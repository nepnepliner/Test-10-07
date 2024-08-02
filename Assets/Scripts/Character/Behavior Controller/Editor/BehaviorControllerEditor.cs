using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BehaviorController))]
public class BehaviorControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BehaviorSolution currentBehavior = (target as BehaviorController).CurrentBehavior;
        EditorGUILayout.LabelField(currentBehavior != null ? currentBehavior.ToString() : "No Behavior Solution") ;
    }
}
