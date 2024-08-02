using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthContainer : MonoBehaviour, ISaveable
{
    [SerializeField] private float _health;
    [SerializeField][Range(0, 1)] private float _healthPercent;
    [SerializeField] private float _healthMax;
    [SerializeField] private float _healthRegen;

    [Header("UI")]
    [SerializeField] private Slider _healthBarUI;

    private void Start()
    {
        HealthFromPercent();
    }

    private void Update()
    {
        Regen();
    }

    private void Regen()
    {
        if (Health > 0)
            Health += _healthRegen * Time.deltaTime;
    }

    public float Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, _healthMax);
            HealthToPercent();
            UpdateHealthBar();
        }
    }

    public float HealthPercent
    {
        get { return _healthPercent; }
        set
        {
            _healthPercent = Mathf.Clamp01(value);
            HealthFromPercent();
            UpdateHealthBar();
        }
    }

    public float HealthMax
    {
        get { return _healthMax; }
        set
        {
            _healthMax = Mathf.Max(0, value);
            Health = Mathf.Clamp(Health, 0, _healthMax);
        }
    }

    public float HealthRegen { get => _healthRegen; set => _healthRegen = value; }

    private void HealthToPercent()
    {
        _healthPercent = _healthMax > 0 ? _health / _healthMax : 0;
    }

    private void HealthFromPercent()
    {
        _health = _healthMax > 0 ? _healthPercent * _healthMax : 0;
    }

    private void UpdateHealthBar()
    {
        if (_healthBarUI != null)
            _healthBarUI.value = _healthPercent;
    }

    public string Save()
    {
        HealthContainerSaveData saveData = new HealthContainerSaveData
        {
            Health = _health
        };
        return JsonUtility.ToJson(saveData);
    }

    public void Load(string jsonSaveData)
    {
        HealthContainerSaveData saveData = JsonUtility.FromJson<HealthContainerSaveData>(jsonSaveData);
        _health = saveData.Health;
    }

    public string GetGUID()
    {
        return this.GUID();
    }
}

[System.Serializable]
public struct HealthContainerSaveData
{
    public float Health;
}
