using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public void Save()
    {
        SaveManager.Instance.Save();
    }

    public void Load()
    {
        SaveManager.Instance.Load();
    }
}
