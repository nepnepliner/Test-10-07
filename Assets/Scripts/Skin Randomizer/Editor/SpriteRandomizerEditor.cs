using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteRandomizer))]
[CanEditMultipleObjects]
public class SpriteRandomizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SpriteRandomizer randomizer = target as SpriteRandomizer;

        DrawDefaultInspector();

        if (GUILayout.Button("Randomize"))
            randomizer.RandomizeSprite();
    }
}
