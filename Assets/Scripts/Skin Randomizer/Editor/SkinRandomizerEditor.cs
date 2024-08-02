using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkinRandomizer))]
[CanEditMultipleObjects]
public class SkinRandomizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SkinRandomizer randomizer = target as SkinRandomizer;

        DrawDefaultInspector();

        if (GUILayout.Button("Randomize"))
            randomizer.RandomizeSkin();
    }
}
