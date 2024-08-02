using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private HealthContainer _healthContainer;
    private Animator _animator;
    private ViewSensor _viewSensor;

    [Space()]
    [SerializeField] private float _damageFactor;
    [SerializeField] private List<Collider2D> _ignoreColliders;

    private void Start()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _animator = _healthContainer.GetComponent<Animator>();
        _viewSensor = _healthContainer.GetComponent<ViewSensor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageContainer damageContainer = collision.GetComponent<DamageContainer>();
        if (damageContainer == null || _ignoreColliders.Contains(collision))
            return;

        _healthContainer.Health -= damageContainer.Damage * _damageFactor;
        _animator.SetTrigger("Damage");
        _viewSensor.SetTarget(damageContainer.Holder);
    }
}
