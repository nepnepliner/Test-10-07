using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBehavior : BehaviorSolution
{
    //[Header("Components")]
    private Animator _animator;
    private HealthContainer _healthContainer;
    private DropContainer _dropContainer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _healthContainer = GetComponent<HealthContainer>();
        _dropContainer = GetComponent<DropContainer>();
    }

    public override bool IsAdditionBehavior() => false;

    public override bool CheckBehaviorConditions()
    {
        if (_healthContainer == null)
            return false;
        return _healthContainer.Health <= 0;
    }

    protected override IEnumerator Behavior()
    {
        _animator.SetBool("Die", true);
        if (_dropContainer != null)
            _dropContainer.Drop();
        while (true)
            yield return null;
    }
}
