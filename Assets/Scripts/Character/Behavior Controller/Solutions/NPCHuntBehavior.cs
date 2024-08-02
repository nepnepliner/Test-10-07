using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHuntBehavior : BehaviorSolution
{
    //[Header("Components")]
    private Animator _animator;
    private ViewSensor _viewSensor;
    private MoveController _moveController;
    private LookAtController _lookAtController;

    [Header("Hunt")]
    [SerializeField] private float _huntTime;
    [SerializeField] private float _huntTargetMinPosition;

    [Header("Delays")]
    [SerializeField] private float _alarmTime;

    private Vector2 _lastTargetPosition;

    private void Start()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        _animator = GetComponent<Animator>();
        _viewSensor = GetComponent<ViewSensor>();
        _moveController = GetComponent<MoveController>();
        _lookAtController = GetComponent<LookAtController>();
    }

    public override bool IsAdditionBehavior() => false;

    public override bool CheckBehaviorConditions()
    {
        return _viewSensor.HaveAnyTarget;
    }

    protected override IEnumerator Behavior()
    {
        UpdateLastTargetPosition();
        ControlsToTarget();
        _moveController.Mobility = 0;
        _animator.SetTrigger("Alarm");
        yield return StartCoroutine(Timer(_alarmTime));
        _moveController.Mobility = 1;
        float huntTimer = _huntTime;

        while (huntTimer > 0 && _stopBehavior == false)
        {
            if (_viewSensor.HaveAnyTarget)
            {
                UpdateLastTargetPosition();
                huntTimer = _huntTime;
            }
            else
            {
                huntTimer -= Time.deltaTime;
            }
            if (Vector2.Distance(transform.position, _lastTargetPosition) < _huntTargetMinPosition)
                _moveController.Mobility = 0.5f;
            else
                _moveController.Mobility = 1;
            ControlsToTarget();
            yield return null;
        }
    }

    private void UpdateLastTargetPosition()
    {
        _lastTargetPosition = _viewSensor.NearestTarget.position;
    }

    private void ControlsToTarget()
    {
        Vector2 targetDir = (_lastTargetPosition - (Vector2)transform.position).normalized;
        _moveController.Direction = targetDir;
        _lookAtController.LookAtDirection(targetDir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying && IsBehaviorRunning)
        {
            Gizmos.DrawWireSphere(_lastTargetPosition, _huntTargetMinPosition);
            Gizmos.DrawLine(_lastTargetPosition, transform.position);
        }
    }
}
