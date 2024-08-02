using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorController : MonoBehaviour
{
    [SerializeField] private BehaviorSolution[] _behaviors;

    private BehaviorSolution _currentBehavior;

    public BehaviorSolution CurrentBehavior => _currentBehavior;

    private void Start()
    {
        InvokeRepeating("UpdateBaseBehaviors", 0, 0.2f);
        InvokeRepeating("UpdateAdditionBehaviors", 0, 0.2f);
    }

    private void UpdateBaseBehaviors()
    {
        foreach (BehaviorSolution behavior in _behaviors)
            if (behavior.IsAdditionBehavior() == false)
            {
                if (behavior.IsBehaviorRunning)
                {
                    _currentBehavior = behavior;
                    break;
                }
                if (behavior.CheckBehaviorConditions())
                {
                    StopAllBaseBehaviors();
                    behavior.StartBehavior();
                    _currentBehavior = behavior;
                    break;
                }
            }
    }

    private void UpdateAdditionBehaviors()
    {
        foreach (BehaviorSolution behavior in _behaviors)
            if (behavior.IsAdditionBehavior())
                if (behavior.CheckBehaviorConditions() && behavior.IsBehaviorRunning == false)
                    behavior.StartBehavior();
    }

    private void StopAllBaseBehaviors()
    {
        foreach (BehaviorSolution behavior in _behaviors)
            if (behavior.IsAdditionBehavior() == false)
                behavior.StopBehavior();
    }
}

