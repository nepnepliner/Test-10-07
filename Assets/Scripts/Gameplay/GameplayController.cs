using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : Singleton<GameplayController>
{
    

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {

    }

    private void Update()
    {
        HandleGameplay();
    }

    private void HandleGameplay()
    {
        
    }

    public void GameEnd()
    {

    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

