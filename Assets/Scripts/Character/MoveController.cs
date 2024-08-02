using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    //[Header("Components")]
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    //[Header("Options")]
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speed;

    [SerializeField] [Range(0, 1)] private float _smoothness;
    [SerializeField] [Range(0, 1)] private float _mobility;

    [Header("Direction Correction")]
    [SerializeField] private bool _useDirectionCorrection;
    [SerializeField] private float _directionCorrectionSence;
    [SerializeField] private float _directionCorrectionRate;
    [SerializeField] private LayerMask _directionCorrectionMask;

    [Header("Animated")]
    [SerializeField] [Range(0, 1)] private float _animFactor;

    //[Header("Last Frame")]
    private Vector2 _lastMovement;
    private Vector2 _lastPosition;

    public Vector2 Direction { get => _direction; set => _direction = value; }
    public float Speed { get => _speed; set => _speed = value; }

    public float Mobility { get => _mobility; set => _mobility = Mathf.Clamp01(value); }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_useDirectionCorrection)
            CorrectDirection();
        Move();
        UpdateAnim();
    }

    private void Move()
    {
        if (_direction == Vector2.zero || _speed == 0 || _rigidbody2D == null)
            return;
        _direction.Normalize();
        Vector2 movement = _direction * _speed;
        movement = Vector2.Lerp(movement, _lastMovement, _smoothness);
        _rigidbody2D.MovePosition(_rigidbody2D.position + movement * _mobility * _animFactor * Time.fixedDeltaTime);
        _lastMovement = movement;
    }

    private void CorrectDirection()
    {
        if (_directionCorrectionSence <= 0 || _directionCorrectionRate <= 0)
            return;
        Vector2 rayDir = _direction.normalized;
        float rayDist = _speed * _directionCorrectionSence;
        if (rayDist == 0)
            return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayDist, _directionCorrectionMask);
        Debug.DrawRay(transform.position, rayDir * rayDist, Color.white);
        if (hit.collider == null)
            return;
        float correctionAngle = 90 * (1 - hit.distance / rayDist);

        Vector2 sideRayDirL = rayDir.Rotate(correctionAngle);
        Vector2 sideRayDirR = rayDir.Rotate(-correctionAngle);
        RaycastHit2D sideHitL = Physics2D.Raycast(transform.position, sideRayDirL, rayDist, _directionCorrectionMask);
        RaycastHit2D sideHitR = Physics2D.Raycast(transform.position, sideRayDirR, rayDist, _directionCorrectionMask);
        Debug.DrawRay(transform.position, sideRayDirL * rayDist, Color.white);
        Debug.DrawRay(transform.position, sideRayDirR * rayDist, Color.white);
        float sideHitDistL = sideHitL ? sideHitL.distance : rayDist;
        float sideHitDistR = sideHitR ? sideHitR.distance : rayDist;
        float deltaAngle = (sideHitDistL < sideHitDistR ? -correctionAngle : correctionAngle) * _directionCorrectionRate * Time.fixedDeltaTime;
        _direction = rayDir.Rotate(deltaAngle);
    }

    private void UpdateAnim()
    {
        Vector2 realMovement = (Vector2)transform.position - _lastPosition;
        Vector2 localMovement = transform.InverseTransformVector(realMovement);
        float move = (realMovement.magnitude + Mathf.Abs(realMovement.x)) * 0.5f / (_speed * Time.fixedDeltaTime);
        if (localMovement.x < 0)
            move *= -1;
        /*if (name == "Player")
            Debug.Log("Move " + move + " " + Time.fixedDeltaTime);*/
        _animator.SetFloat("Move", move);
        _lastPosition = transform.position;
    }
}

