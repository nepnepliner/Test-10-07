using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    //[Header("Components")]
    private Camera _camera;
    private Transform _player;

    [SerializeField] private float _speed;
    [SerializeField] private float _advanceFactor;

    private Vector2 _lastPlayerPosition;
    private Vector2 _targetPosition;

    private void Start()
    {
        _camera = Camera.main;
        _player = FindObjectOfType<PlayerController>().transform;
        _lastPlayerPosition = _player.position;
    }

    private void FixedUpdate()
    {
        Vector2 playerPosition = _player.position;
        _targetPosition = playerPosition + (playerPosition - _lastPlayerPosition) * _advanceFactor / Time.fixedDeltaTime;
        _lastPlayerPosition = playerPosition;
    }

    private void Update()
    {
        Vector3 position = Vector3.Lerp(_camera.transform.position,
                                        (Vector3)_targetPosition + new Vector3(0, 0, _camera.transform.position.z),
                                        _speed * Time.deltaTime);
        Location location = Location.Instance;
        _camera.transform.position = location.ClampPosition(position, location.PossibleArea(CameraRealSize()));
    }

    private Vector2 CameraRealSize()
    {
        float height = 2f * _camera.orthographicSize;
        float width = height * _camera.aspect;
        return new Vector2(width, height);
    }
}
