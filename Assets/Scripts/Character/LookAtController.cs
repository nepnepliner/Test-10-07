using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtController : MonoBehaviour
{
    //[SerializeField] private Transform _transform;
    [SerializeField] private bool _flip;

    //private Vector3 _rootTransformScale;
    private bool _side;

    private void Start()
    {
        //_rootTransformScale = transform.localScale;
        UptaleTransform();
    }

    public void LookAtPoint(Vector2 lookAtPoint)
    {
        LookAtDirection(lookAtPoint - (Vector2)transform.position);
    }

    public void LookAtDirection(Vector2 lookAtDir)
    {
        if (transform.parent != null)
            transform.parent.InverseTransformDirection(lookAtDir);
        _side = lookAtDir.x > 0;
        UptaleTransform();
    }

    private void UptaleTransform()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (_side != _flip ? -1 : 1),
                                           transform.localScale.y,
                                           transform.localScale.z);
    }
}
