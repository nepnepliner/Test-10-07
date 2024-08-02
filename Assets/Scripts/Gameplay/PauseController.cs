using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : Singleton<PauseController>
{
    private bool _isGamePaused;

    public bool IsGamePause { get => _isGamePaused; set => SetGamePause(value); }

    public void SetGamePause(bool pause)
    {
        _isGamePaused = pause;
        Time.timeScale = pause ? 0 : 1;
    }

    public void ToggleGamePause() => SetGamePause(!_isGamePaused);
}
