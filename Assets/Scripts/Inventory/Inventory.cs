using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Inventory : MonoBehaviour, ISaveable
{
    // [Header("Components")]
    private ItemDropper _dropper;

    [SerializeField] private List<ItemSlot> _slots;

    [Header("UI")]
    [SerializeField] private Transform _slotsUIContainer;

    private void Start()
    {
        InitComponents();
        StartCoroutine(InitSlots());
    }

    private void InitComponents()
    {
        _dropper = GetComponent<ItemDropper>();
    }

    private IEnumerator InitSlots()
    {
        yield return new WaitForSeconds(1.0f);
        InventorySlotUI[] slotsUI = _slotsUIContainer.GetComponentsInChildren<InventorySlotUI>(true);

        for (int i = 0; i < slotsUI.Length; i++)
        {
            if (_slots.Count < i + 1)
                _slots.Add(new ItemSlot());
            else if (_slots[i] == null)
                _slots[i] = new ItemSlot();
            _slots[i].SlotUI = slotsUI[i];
            _slots[i].Init();
            slotsUI[i].OnClearButtonDown += _slots[i].Clear;
        }

        while (_slots.Count > slotsUI.Length)
        {
            ItemSlot slot = _slots.Last();
            if (slot.Item != null)
                _dropper.Drop(slot.Item, slot.Count);

            _slots.RemoveAt(_slots.Count - 1);
        }
    }

    public int TryAdd(Item item, int itemsCount = 1)
    {
        int successCount = 0;
        for (int i = 0; i < _slots.Count && successCount < itemsCount; i++)
        {
            successCount += _slots[i].TryAdd(item, itemsCount - successCount);
        }
        return successCount;
    }

    public int TryRemove(Item item, int itemsCount = 1)
    {
        int successCount = 0;
        for (int i = 0; i < _slots.Count && successCount < itemsCount; i++)
        {
            successCount += _slots[i].TryRemove(item, itemsCount - successCount);
        }
        return successCount;
    }

    public string Save()
    {
        InventorySaveData saveData = new InventorySaveData
        {
            Slots = _slots
        };
        return JsonUtility.ToJson(saveData);
    }

    public void Load(string jsonSaveData)
    {
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(jsonSaveData);
        _slots = saveData.Slots;
    }

    public string GetGUID()
    {
        return this.GUID();
    }
}

[System.Serializable]
public struct InventorySaveData
{
    public List<ItemSlot> Slots;
}
