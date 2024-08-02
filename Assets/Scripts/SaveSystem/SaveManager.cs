using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class SaveManager : Singleton<SaveManager>
{
    private string SavePath => Application.persistentDataPath + "/Save.json";

    public void Save()
    {
        string saveData = "";
        IEnumerable<ISaveable> saveables = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
        foreach (ISaveable saveable in saveables)
        {
            string saveKey = saveable.GetGUID();
            saveData += $"{saveKey}:{saveable.Save()}\n";
        }
        File.WriteAllText(SavePath, saveData);
    }

    public void Load()
    {
        if (File.Exists(SavePath))
        {
            string saveData = File.ReadAllText(SavePath);
            IEnumerable<ISaveable> saveables = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
            foreach (ISaveable saveable in saveables)
            {
                string saveKey = saveable.GetGUID();
                foreach (string line in saveData.Split('\n'))
                    if (line.StartsWith(saveKey))
                    {
                        saveable.Load(line.Substring(saveKey.Length + 1));
                        break;
                    }
            }
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }
}


