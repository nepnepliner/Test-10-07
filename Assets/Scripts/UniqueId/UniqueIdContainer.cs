using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class UniqueIdContainer : MonoBehaviour
{
    [SerializeField] [HideInInspector] private int uniqueId = -1;

    public int UniqueId => uniqueId;

#if UNITY_EDITOR
    private void OnValidate()
    {
        GenerateUniqueId();
    }
#endif

    private void Awake()
    {
        GenerateUniqueId();
    }

    private void GenerateUniqueId()
    {
        List<int> allIds = FindObjectsOfType<UniqueIdContainer>()
                            .Where(obj => obj != this)
                            .Select(obj => obj.uniqueId)
                            .ToList();
        int newId = 0;
        while (allIds.Contains(newId))
            newId++;
        uniqueId = newId;
        // Debug.Log($"Generated Unique ID: {uniqueId} for {gameObject.name}");
    }
}


