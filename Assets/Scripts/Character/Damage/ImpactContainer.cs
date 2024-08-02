using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactContainer : MonoBehaviour
{
    [SerializeField] private float _impact;
    [SerializeField] private Vector2 _impactPivot;

    private Vector2 WorldImpactPivot => transform.TransformPoint(_impactPivot);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D otherRigidbody2D = collision.GetComponent<Rigidbody2D>();
        if (otherRigidbody2D == null)
            return;

        Vector2 impactDir = ((Vector2)collision.transform.position - WorldImpactPivot).normalized;
        otherRigidbody2D.AddForce(impactDir * _impact, ForceMode2D.Impulse);
    }
}
