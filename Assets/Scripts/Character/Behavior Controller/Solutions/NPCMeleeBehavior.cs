using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMeleeBehavior : BehaviorSolution
{
    // [Header("Components")]
    private MeleeController _meleeController;

    private void Start()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _meleeController = GetComponent<MeleeController>();
    }

    public override bool IsAdditionBehavior() => true;

    public override bool CheckBehaviorConditions()
    {
        return _meleeController.HaveTarget && _meleeController.IsAttacking == false;
    }

    protected override IEnumerator Behavior()
    {
        _meleeController.Attack();
        while (_meleeController.IsAttacking)
            yield return null;
    }
}
