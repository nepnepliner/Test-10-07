using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemContainer : MonoBehaviour
{
    private static float _startBlockingDelay = 0.5f;

    // [Header("Components")]
    private Animator _animator;

    [SerializeField] private ItemSlot _slot;
    [SerializeField] private bool _isBlocked;

    public Item Item => _slot.Item;
    public int Count => _slot.Count;
    public bool IsBlocked { get => _isBlocked; set => _isBlocked = value; }

    private void Start()
    {
        InitComponents();
        StartCoroutine(OnStartBlocking());
    }

    private void InitComponents()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator OnStartBlocking()
    {
        if (_isBlocked)
            yield return null;
        else
        {
            _isBlocked = true;
            yield return new WaitForSeconds(_startBlockingDelay);
            _isBlocked = false;
        }
    }

    public int TryAdd(Item item, int count)
    {
        return _slot.TryAdd(item, count);
    }

    public int TryRemove(Item item, int count)
    {
        int successCount = _slot.TryRemove(item, count);
        if (_slot.IsContainAny == false)
            Destroy();
        return successCount;
    }

    public void Destroy()
    {
        _isBlocked = true;
        if (_animator != null)
            _animator.SetTrigger("OnEmpty");
        else
            Destroy(this.gameObject, 0.5f);
    }

    public void ClampCount() => _slot.ClampCount();
    public void UpdateUI() => _slot.UpdateUI();
}
