using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySafer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
