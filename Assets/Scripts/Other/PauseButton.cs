using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void SetGamePause(bool pause)
    {
        PauseController.Instance.SetGamePause(pause);
    }

    public void ToggleGamePause()
    {
        PauseController.Instance.ToggleGamePause();
    }
}
