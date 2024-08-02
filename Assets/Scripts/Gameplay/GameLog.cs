using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLog : Singleton<GameLog>
{
    [Header("Components")]
    [SerializeField] private Animation _animator;

    private List<string> _logEntries = new List<string>();

    public void Log(string message)
    {
        _animator.Sample();
        _logEntries.Add(message);
        Debug.Log(message);
    }

    public void ClearLog()
    {
        _logEntries.Clear();
    }

    public List<string> GetLogEntries()
    {
        return new List<string>(_logEntries);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 500, 500));
        GUILayout.Label("Game Log:");
        foreach (string entry in _logEntries)
        {
            GUILayout.Label(entry);
        }
        GUILayout.EndArea();
    }
}

