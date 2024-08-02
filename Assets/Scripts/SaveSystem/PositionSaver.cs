using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSaver : MonoBehaviour, ISaveable
{
    public string Save()
    {
        PositionSaveData saveData = new PositionSaveData
        {
            Position = transform.position
        };
        return JsonUtility.ToJson(saveData);
    }

    public void Load(string jsonSaveData)
    {
        Debug.Log(jsonSaveData);
        PositionSaveData saveData = JsonUtility.FromJson<PositionSaveData>(jsonSaveData);
        Debug.Log(saveData.Position);
        transform.position = saveData.Position;
    }

    public string GetGUID()
    {
        return this.GUID();
    }
}

[System.Serializable]
public struct PositionSaveData
{
    public Vector3 Position;
}
