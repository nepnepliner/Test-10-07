using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
[ExecuteInEditMode]
public class PositionTransparencySorter : LocalTransparencySorter
{
    private static float _positionFactor = 0.1f;

    [SerializeField] private Vector2 _pivot;

    public Vector2 WorldPivot 
    { 
        get
        {
            return transform.TransformPoint(_pivot);
        }
        set
        {
            _pivot = transform.InverseTransformPoint(value);
            UpdateTransparency();
        }
    }

    public override void UpdateTransparency()
    {
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y,
                                         FindPositoinOffset() + FindOrderOffset());
        //Debug.Log(name + " [Position Sort Transparency]");
    }

    private float FindPositoinOffset()
    {
        return WorldPivot.y * _positionFactor;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(WorldPivot, 0.1f);
    }
}
