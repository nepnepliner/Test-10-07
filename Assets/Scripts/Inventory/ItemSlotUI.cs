using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    // [Header("Components")]
    private Animator _animator;

    [SerializeField] protected Image _itemIconUI;
    [SerializeField] protected TextMeshProUGUI _itemCountUI;
    [SerializeField] protected bool _isControllingHide;

    private void Start()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetData(Item item, int count)
    {
        if (item == null || count <= 0)
        {
            SetCount(0);
            HideUI();
        }
        else
        {
            SetIcon(item.Sprite);
            SetCount(count);
            ShowUI();
        }
    }

    private void SetIcon(Sprite sprite) => _itemIconUI.sprite = sprite;

    private void SetCount(int count) => _itemCountUI.text = count > 1 ? count.ToString() : "";

    private void HideUI()
    {
        SetVisible(false);
    }

    private void ShowUI()
    {
        SetVisible(true);
    }

    private void SetVisible(bool visible)
    {
        if (_isControllingHide == false)
            return;

        if (_animator != null)
            _animator.SetBool("HaveItem", visible);
    }
}
