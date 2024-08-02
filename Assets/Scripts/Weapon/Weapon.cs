using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponShootingMode { Single, Double, Automatic }

//[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    private static float _maxShootDist = 100;

    [Header("Components")]
    [SerializeField] private Animator _animator;

    [Header("Options")]
    [SerializeField] private float _damage;
    //[SerializeField] private string _weaponType; // for Animator
    [SerializeField] private Item _ammoType;
    [SerializeField] private WeaponShootingMode _shootingMode;
    [SerializeField] private float _shootingFrequency;
    [SerializeField] private float _shootingSpreadAngle;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Vector2 _barrelPivot;

    [Header("Mask")]
    [SerializeField] private LayerMask _shootingMask;

    public Item AmmoType => _ammoType;
    public int ShootsLimits
    {
        get
        {
            switch (_shootingMode)
            {
                case WeaponShootingMode.Single:
                    return 1;
                case WeaponShootingMode.Double:
                    return 2;
                case WeaponShootingMode.Automatic:
                    return int.MaxValue;
                default:
                    return 0;
            }
        }
    }
    private float ShootingRrequency => _shootingFrequency;



    private void Start()
    {
        //_animator = GetComponent<Animator>();
    }

    private Vector2 BarrelWorldPivot => transform.TransformPoint(_barrelPivot);

    public void Shoot(Vector2 targetPoint, float spreadFactor = 1)
    {
        Vector2 originPoint = BarrelWorldPivot;
        Vector2 targetDir = (targetPoint - originPoint).normalized;
        float randomSpreadAngle = (Random.value - 0.5f) * spreadFactor * _shootingSpreadAngle;
        targetDir = targetDir.Rotate(randomSpreadAngle);
        RaycastHit2D hit = Physics2D.Raycast(originPoint, targetDir, _maxShootDist, _shootingMask);
        if (hit == false)
            return;
        Debug.DrawLine(originPoint, hit.point, Color.magenta, 0.5f);
        GameObject bullet = Instantiate(_bulletPrefab, hit.point, Quaternion.identity);
        bullet.GetComponent<DamageContainer>().Damage = _damage;
        _animator.SetTrigger("Shoot");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(BarrelWorldPivot, 0.05f);
    }
}
