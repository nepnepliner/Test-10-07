using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ViewSensorSide { Left, Right, Both }

public class ViewSensor : MonoBehaviour
{
    [SerializeField] private Vector2 _viewPivot;
    [SerializeField] private float _viewRadius;
    [SerializeField] [Range(0, 90)] private float _viewIncline;
    [SerializeField] private ViewSensorSide _viewSide;

    [Header("Mask")]
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    private List<Transform> _visibleTargets = new List<Transform>();
    private Transform _nearestTarget;

    public bool HaveAnyTarget => _visibleTargets.Count > 0;

    public int TargetsCount => _visibleTargets.Count;

    public Transform NearestTarget => _nearestTarget;

    public void SetTarget(Transform target)
    {
        _visibleTargets.Clear();
        _visibleTargets.Add(target);
        _nearestTarget = target;
    }

    private float ViewInclineFactor => Mathf.Cos(_viewIncline * Mathf.Deg2Rad);

    private Vector2 WorldPivot => transform.TransformPoint(_viewPivot);

    private void Start()
    {
        InvokeRepeating("FindTargets", 0, 0.2f);
    }


    private void FindTargets()
    {
        UpdateVisibleTargets();
        UpdateNearestTagret();
    }

    private void UpdateVisibleTargets()
    {
        _visibleTargets.Clear();
        Vector2 worldPivot = WorldPivot;
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCapsuleAll(worldPivot, new Vector2(_viewRadius, _viewRadius * ViewInclineFactor) * 2, CapsuleDirection2D.Horizontal, 0, _targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector2 dirToTarget = ((Vector2)target.position - worldPivot).normalized;
            bool side = transform.InverseTransformVector(dirToTarget).x > 0;
            if ((_viewSide == ViewSensorSide.Right && side == false) ||
                (_viewSide == ViewSensorSide.Left && side == true))
                continue;
            float distToTarget = Vector2.Distance(transform.position, target.position);
            if (target.GetComponent<DamageReceiver>() == false)
                continue;
            if (Physics2D.Raycast(worldPivot, dirToTarget, distToTarget, _obstacleMask) == false)
                _visibleTargets.Add(target);
        }
    }

    private void UpdateNearestTagret()
    {
        if (HaveAnyTarget == false)
        {
            _nearestTarget = null;
            return;
        }
        Vector2 worldPivot = WorldPivot;
        _nearestTarget = _visibleTargets[0];
        float minDist = float.MaxValue;
        foreach (Transform target in _visibleTargets)
        {
            float dist = Vector2.Distance(target.position, worldPivot);
            if (dist < minDist)
            {
                _nearestTarget = target;
                minDist = dist;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        foreach (Transform visibleTarget in _visibleTargets)
        {
            Gizmos.DrawLine(transform.position, visibleTarget.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector2 worldPivot = WorldPivot;
        float x = _viewRadius;
        float y = _viewRadius * ViewInclineFactor;
        float d = x - y;

        Gizmos.DrawWireSphere(worldPivot + Vector2.right * d, y);
        Gizmos.DrawWireSphere(worldPivot + Vector2.left * d, y);
        Gizmos.DrawLine(worldPivot + new Vector2(-d, y), worldPivot + new Vector2(d, y));
        Gizmos.DrawLine(worldPivot + new Vector2(-d, -y), worldPivot + new Vector2(d, -y));
    }
}

