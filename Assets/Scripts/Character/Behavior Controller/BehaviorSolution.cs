using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorSolution : MonoBehaviour
{

    private Coroutine _behaviorCoroutine;
    private Coroutine _useBehaviorCoroutine;

    protected bool _stopBehavior;

    public bool IsBehaviorRunning => _behaviorCoroutine != null;

    public abstract bool IsAdditionBehavior();
    public abstract bool CheckBehaviorConditions();
    protected abstract IEnumerator Behavior();

    public void StartBehavior(bool forceRestart = false)
    {
        if (IsBehaviorRunning)
            if (forceRestart)
                ForceStopBehavior();
            else
                return;

        _useBehaviorCoroutine = StartCoroutine(UseBehavior());
    }

    public void StopBehavior(bool forceStop = false)
    {
        if (IsBehaviorRunning)
            if (forceStop)
                ForceStopBehavior();

        _stopBehavior = true;
    }

    private void ForceStopBehavior()
    {
        StopCoroutine(_behaviorCoroutine);
        _behaviorCoroutine = null;
        StopCoroutine(_useBehaviorCoroutine);
        _useBehaviorCoroutine = null;
        _stopBehavior = false;
    }

    private IEnumerator UseBehavior()
    {
        _stopBehavior = false;
        yield return _behaviorCoroutine = StartCoroutine(Behavior());
        _behaviorCoroutine = null;
        _useBehaviorCoroutine = null;
        _stopBehavior = false;
    }

    protected IEnumerator Timer(float timer)
    {
        while (timer > 0 && _stopBehavior == false)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
