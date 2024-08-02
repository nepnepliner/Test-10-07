using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContainer : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private Transform _holder;

    public float Damage { get => _damage; set => _damage = value; }
    public Transform Holder { get => _holder != null ? _holder : transform; set => _holder = value; }
}
