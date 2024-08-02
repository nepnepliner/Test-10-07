using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventorySlotUI : ItemSlotUI
{
    public Action OnClearButtonDown;

    public void ClearButtonDown()
    {
        OnClearButtonDown.Invoke();
    }
}
