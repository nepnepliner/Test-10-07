using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomTransparencySorter : MonoBehaviour
{
    [SerializeField] protected bool _isUpdating;

    public abstract void UpdateTransparency();

    private void LateUpdate()
    {
        if (_isUpdating && transform.hasChanged)
        {
            UpdateTransparency();
        }
    }
}
