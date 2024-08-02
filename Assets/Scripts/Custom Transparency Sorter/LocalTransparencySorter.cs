using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
[ExecuteInEditMode]
public class LocalTransparencySorter : CustomTransparencySorter
{
    private static float _orderFactor = -0.001f;

    [SerializeField] private int _order;

    public int Order => _order;

    public override void UpdateTransparency()
    {
        float origin = transform.parent != null ? transform.parent.position.z : 0;
        //Vector3 dir = transform.parent != null ? -transform.forward : -Vector3.forward;
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y,
                                         origin + FindOrderOffset());
        //Debug.Log(name + " [Local Sort Transparency]");
    }

    protected float FindOrderOffset()
    {
        //Debug.Log(Order + " " + Order * _orderFactor);
        return Order * _orderFactor;
    }
}
