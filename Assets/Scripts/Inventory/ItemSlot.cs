using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ItemSlot
{
    private static int _maxItemCount = 99;

    [SerializeField] private Item _item;
    [SerializeField] private int _count;

    [Header("UI")]
    [SerializeField] private ItemSlotUI _slotUI;

    public Item Item => _item;
    public int Count => _count;
    public ItemSlotUI SlotUI { set => _slotUI = value; }

    public bool IsContainAny => _item != null && _count > 0;

    public void Init()
    {
        ClampCount();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (_slotUI != null)
            _slotUI.SetData(_item, _count);
    }

    public int TryAdd(Item item, int itemsCount = 1)
    {
        if (item == null || itemsCount <= 0)
            return 0;

        int successCount = 0;
        if (IsContainAny == false)
        {
            _item = item;
            successCount = TrySetCount(itemsCount);
            UpdateUI();
        }
        else if (_item == item)
        {
            successCount = TrySetCount(_count + itemsCount);
            UpdateUI();
        }

        return successCount;
    }

    public int TryRemove(Item item, int itemsCount = 1)
    {
        if (item == null || itemsCount <= 0)
            return 0;

        int successCount = 0;
        if (_item == item)
        {
            successCount = -TrySetCount(_count - itemsCount);
            UpdateUI();
        }

        return successCount;
    }

    public void Clear()
    {
        _item = null;
        _count = 0;
        UpdateUI();
    }

    public void ClampCount()
    {
        _count = Mathf.Clamp(_count, 0, _maxItemCount);
    }

    private int TrySetCount(int count)
    {
        int buf = _count;
        _count = count;
        ClampCount();
        return _count - buf;
    }
}

