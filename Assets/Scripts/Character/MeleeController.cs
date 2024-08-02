using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeleeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private DamageContainer _damageContainer;
    private Animator _animator;

    [Space()]
    [SerializeField] private float _damage;
    [SerializeField] private float _attackTime;

    [Header("Attack Trigger")]
    [SerializeField] private Vector2 _attactTriggerPivot;
    [SerializeField] private Vector2 _attactTriggerSize;
    [SerializeField] private LayerMask _attactTriggerMask;

    private Transform _target;

    public bool HaveTarget => _target != null;

    public Transform Target => _target;

    private bool _isAttacking;

    public bool IsAttacking => _isAttacking;

    private void Start()
    {
        InitComponents();
        InvokeRepeating("CheckTarget", 0, 0.2f);
    }

    private void InitComponents()
    {
        _animator = GetComponent<Animator>();
    }

    private Vector2 WorldAttactTriggerPivot => transform.TransformPoint(_attactTriggerPivot);

    public void CheckTarget()
    {
        Collider2D[] otherColliders = Physics2D.OverlapCapsuleAll(WorldAttactTriggerPivot,
                                              _attactTriggerSize,
                                              _attactTriggerSize.y > _attactTriggerSize.x ? CapsuleDirection2D.Vertical :
                                                                                            CapsuleDirection2D.Horizontal,
                                              0,
                                              _attactTriggerMask);
        DamageReceiver[] damageReceivers = otherColliders.Select(x => x.GetComponent<DamageReceiver>()).Where(x => x != null).ToArray();
        _target = damageReceivers.Length > 0 ? damageReceivers[0].transform : null;
    }
        


    public void Attack()
    {
        if (_isAttacking == false)
            StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        _animator.SetTrigger("Attack");
        _isAttacking = true;
        yield return new WaitForSeconds(_attackTime);
        _isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        GizmosUtilities.DrawCapsule(WorldAttactTriggerPivot, _attactTriggerSize);
    }
}
