using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScailer : MonoBehaviour
{
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _inputSetsitivity;

    private void Start()
    {
        CalmpScale();
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
            ChangeScale(Input.mouseScrollDelta.y);
    }

    private void ChangeScale(float delta)
    {
        Camera.main.orthographicSize -= delta * _inputSetsitivity * Time.deltaTime;
        CalmpScale();
    }

    private void CalmpScale()
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, _minScale, _maxScale);
    }
}
