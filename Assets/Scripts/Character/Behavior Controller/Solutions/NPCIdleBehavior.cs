using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IdleBehaviorPhase { Stay, Walk, Action }

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(LookAtController))]
public class NPCIdleBehavior : BehaviorSolution
{
    //[Header("Components")]
    private Animator _animator;
    private MoveController _moveController;
    private LookAtController _lookAtController;


    [SerializeField] [Range(0, 1)] private float _stayPriority;
    [SerializeField] [Range(0, 1)] private float _walkPriority;
    [SerializeField] [Range(0, 1)] private float _actionPriority;
    [SerializeField] private float _stayTime;
    [SerializeField] private float _walkTime;
    [SerializeField] private float _actionTime;
    [SerializeField] [Range(0, 1)] private float _walkSpeedFactor;
    //[SerializeField] private float _walkVelosity = 5.0f;
    [SerializeField] private int _walkAreaRadius;

    private IdleBehaviorPhase _phase;
    private Vector2 _walkAreaPositon;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _moveController = GetComponent<MoveController>();
        _lookAtController = GetComponent<LookAtController>();
        _walkAreaPositon = transform.position;
    }

    public override bool IsAdditionBehavior() => false;

    public override bool CheckBehaviorConditions()
    {
        return true;
    }

    protected override IEnumerator Behavior()
    {
        while (_stopBehavior == false)
        {
            _phase = RandomPhase();
            switch (_phase)
            {
                case IdleBehaviorPhase.Stay:
                    yield return StartCoroutine(Stay());
                    break;
                case IdleBehaviorPhase.Walk:
                    yield return StartCoroutine(Walk());
                    break;
                case IdleBehaviorPhase.Action:
                    yield return StartCoroutine(Action());
                    break;
            }
        }
    }

    private IEnumerator Stay()
    {
        _moveController.Mobility = 0;
        yield return StartCoroutine(Timer(RandomizeTime(_stayTime)));
    }

    private IEnumerator Walk()
    {
        Vector2 moveTarget = _walkAreaPositon + Random.insideUnitCircle * _walkAreaRadius;
        Vector2 moveDir = (moveTarget - (Vector2)transform.position).normalized;
        _moveController.Direction = moveDir;
        _moveController.Mobility = _walkSpeedFactor;
        _lookAtController.LookAtDirection(moveDir);
        yield return StartCoroutine(Timer(RandomizeTime(_walkTime)));
    }

    private IEnumerator Action()
    {
        _moveController.Mobility = 0;
        _animator.SetTrigger("IdleAction");
        yield return StartCoroutine(Timer(RandomizeTime(_actionTime)));
    }

    private IdleBehaviorPhase RandomPhase()
    {
        float stayRandomPriority = Random.Range(0, _stayPriority);
        float walkRandomPriority = Random.Range(0, _walkPriority);
        float actionRandomPriority = Random.Range(0, _actionPriority);
        float sumPriorities = stayRandomPriority + walkRandomPriority + actionRandomPriority;
        float randomSumPriority = Random.Range(0, sumPriorities);
        if (randomSumPriority < stayRandomPriority)
            return IdleBehaviorPhase.Stay;
        else if (randomSumPriority < walkRandomPriority + stayRandomPriority)
            return IdleBehaviorPhase.Walk;
        else
            return IdleBehaviorPhase.Action;
    }

    private float RandomizeTime(float time)
    {
        return Random.Range(time * 0.5f, time * 1.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (Application.isPlaying && IsBehaviorRunning)
            Gizmos.DrawWireSphere(_walkAreaPositon, _walkAreaRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (Application.isPlaying == false)
            Gizmos.DrawWireSphere(transform.position, _walkAreaRadius);
    }
}
