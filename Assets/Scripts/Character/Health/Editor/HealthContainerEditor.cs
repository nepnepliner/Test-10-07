using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(HealthContainer))]
[CanEditMultipleObjects]
public class HealthContainerEditor : Editor
{
    HealthContainer _healthContainer;

    private void OnEnable()
    {
        _healthContainer = (HealthContainer)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginHorizontal();
        _healthContainer.HealthPercent = EditorGUILayout.Slider(_healthContainer.HealthPercent, 0, 1);
        _healthContainer.Health = EditorGUILayout.FloatField(_healthContainer.Health);
        EditorGUILayout.EndHorizontal();

        _healthContainer.HealthMax = EditorGUILayout.FloatField("Max", _healthContainer.HealthMax);
        _healthContainer.HealthRegen = EditorGUILayout.FloatField("Regen", _healthContainer.HealthRegen);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_healthBarUI"));

        bool autoCh = serializedObject.ApplyModifiedProperties();
        bool custCha = EditorGUI.EndChangeCheck();
        //Debug.Log(autoCh + " " + custCha);
        if (autoCh == false && custCha)
            Undo.RegisterCompleteObjectUndo(target, "HealthContainer Edit");
    }
}
