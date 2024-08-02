using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[Header("Components")]
    private Animator _animator;
    private HealthContainer _healthContainer;
    private ViewSensor _viewSensor;
    private MoveController _moveController;
    private LookAtController _lookAtController;
    private ShootController _shootController;

    [SerializeField] private Joystick _joystick;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _healthContainer = GetComponent<HealthContainer>();
        _viewSensor = GetComponent<ViewSensor>();
        _moveController = GetComponent<MoveController>();
        _lookAtController = GetComponent<LookAtController>();
        _shootController = GetComponent<ShootController>();

        StartCoroutine(PlayerControl());
    }

    private IEnumerator PlayerControl()
    {
        while (_healthContainer.Health > 0)
        {
            if (_joystick.IsActive)
            {
                _moveController.Direction = _joystick.InputVector.normalized;
                _moveController.Mobility = _joystick.InputVector.magnitude;
                _lookAtController.LookAtDirection(_joystick.InputVector.normalized);
            }
            else
            {
                _moveController.Mobility = 0;
            }
            if (_viewSensor.HaveAnyTarget)
                _lookAtController.LookAtPoint(_viewSensor.NearestTarget.position);
            yield return null;
        }
        _moveController.Mobility = 0;
        _shootController.IsActive = false;
        _animator.SetTrigger("Die");
        _animator.SetBool("Stop", true);
        GameplayController.Instance.GameEnd();
    }
}

