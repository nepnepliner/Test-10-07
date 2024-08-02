using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private bool _start;

    public float Timer { get => _timer; set => _timer = value; }

    private void Start()
    {
        if (_start)
            StartTimer();
    }

    public void StartTimer()
    {
        Destroy(this.gameObject, _timer);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
