using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    private static float _spreadFactorStep = 0.33f;

    //[Header("Components")]
    private Animator _animator;
    private ViewSensor _viewSensor;
    private Inventory _inventory;

    [SerializeField] private bool _isActive;

    [Header("Weapon")]
    [SerializeField] private Weapon _weapon;

    [Header("Aiming Bone")]
    [SerializeField] private Transform _aimingBone;
    [SerializeField] [Range(0, 1)] private float _aimingBoneRotateFactor;
    [SerializeField] private float _aimingBoneAngleOffset;

    [Header("UI")]
    [SerializeField] private Button _shootButtonUI;

    [Header("Mask")]
    [SerializeField] private LayerMask _shootingMask;

    private Coroutine _shootCoroutine;
    private float _spreadFactor;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    private void Start()
    {
        _viewSensor = GetComponent<ViewSensor>();
        _animator = GetComponent<Animator>();
        _inventory = GetComponent<Inventory>();
        StartCoroutine(Aiming());
        StartCoroutine(CooldownSpreadFactor());
    }

    private IEnumerator Aiming()
    {
        while (true)
        {
            while (_isActive == false)
                yield return null;

            while (_isActive)
            {
                Aim();
                yield return null;
            }
            ClearAiming();
            yield return null;
        }
    }

    private void ClearAiming()
    {
        _animator.SetBool("Aiming", false);
        SetAimingBoneAngle(0);
    }

    private void Aim()
    {
        bool haveAnyTarget = _viewSensor.HaveAnyTarget;
        _shootButtonUI.interactable = haveAnyTarget;
        _animator.SetBool("Aiming", haveAnyTarget);
        RotateAimingBone();
    }

    private void RotateAimingBone()
    {
        if (_aimingBone == null)
            return;

        float aimingAngle = 0;
        if (_viewSensor.HaveAnyTarget)
        {
            Vector2 aimingDir = (_aimingBone.parent.InverseTransformPoint(_viewSensor.NearestTarget.position)
                                 - _aimingBone.localPosition).normalized;
            aimingAngle = Mathf.Atan2(aimingDir.y, aimingDir.x) * Mathf.Rad2Deg;
            aimingAngle = Mathf.LerpAngle(0, aimingAngle, _aimingBoneRotateFactor);
        }
        SetAimingBoneAngle(aimingAngle);
    }

    private void SetAimingBoneAngle(float aimingAngle)
    {
        _aimingBone.transform.localEulerAngles = Vector3.forward * (aimingAngle + _aimingBoneAngleOffset);
    }

    // Shooting

    public void OnWaeponTriggerDown()
    {
        if (_viewSensor.HaveAnyTarget == false)
            return;

        _shootCoroutine = StartCoroutine(Shooting());
    }

    public void OnWeaponTriggerUp()
    {
        StopCoroutine(_shootCoroutine);
    }

    private IEnumerator Shooting()
    {
        int shootLimits = _weapon.ShootsLimits;

        for (int i = 0; i < shootLimits &&
                       _viewSensor.HaveAnyTarget &&
                       _inventory.TryRemove(_weapon.AmmoType) > 0; i++)
        {
            IncreaseSpreadFactor();
            Shoot();
            yield return null;
        }
    }

    public void Shoot()
    {
        if (_inventory.TryRemove(_weapon.AmmoType) > 0)
            _weapon.Shoot(_viewSensor.NearestTarget.position, 1); // _spreadFactor
    }

    private void IncreaseSpreadFactor()
    {
        _spreadFactor = Mathf.Clamp01(_spreadFactor + _spreadFactorStep);
    }

    private IEnumerator CooldownSpreadFactor()
    {
        while (true)
        {
            _spreadFactor = Mathf.Max(0, _spreadFactor -= Time.deltaTime);
            yield return null;
        }
    }


}
