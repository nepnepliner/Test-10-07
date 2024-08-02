using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    public string Save();

    public void Load(string data);

    public string GetGUID();
}

