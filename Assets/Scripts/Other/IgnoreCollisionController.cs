using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionController : MonoBehaviour
{
    [SerializeField] private Collider2D[] _ignoreColliders;

    private void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
            return;

        foreach (Collider2D ignoreCollider in _ignoreColliders)
        {
            if (ignoreCollider == null)
                continue;
            Physics2D.IgnoreCollision(ignoreCollider, collider);
        }
    }
}
