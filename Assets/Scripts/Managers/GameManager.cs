using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Menu,
    OnGameStart,
    Playing,
    LevelUp,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameStates currentState;
    bool isPlayerDead;

    public PlayerController player;

    public int killCount;

    public int playerScore;

    public bool isFinalWave;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentState = GameStates.Playing;
    }

    private void Update()
    {
        GameStateManager();
    }

    private void GameStateManager()
    {
        if (currentState == GameStates.OnGameStart)
        {
            player = FindFirstObjectByType<PlayerController>();
            currentState = GameStates.Playing;
        }
        if (currentState == GameStates.Playing)
        {
            FindFirstObjectByType<GameplayUI>().SwapUI(false);
            TimerUpdate();
        }
        if (currentState == GameStates.Paused)
        {
            Pause();
        }
        if (currentState == GameStates.GameOver) 
        {
            GameOver(isPlayerDead);
        }

    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume(bool wasPaused)
    {
        //  wasPaused is used for targeting what UI to turn off (Pause menu or Level Up Menu)

        currentState = GameStates.Playing;
        Time.timeScale = 1f;
    }

    public void GameOver(bool hasDied)
    {
        FindFirstObjectByType<GameplayUI>().EndGameText(hasDied);
        FindFirstObjectByType<GameplayUI>().SwapUI(true);

        Time.timeScale = 0;
    }
    #region Game Timer
    //------------------------------------------------------------------------------------
    //                                      Game Timer
    //------------------------------------------------------------------------------------
    
    public bool isTimerRunning;
    public float timer;

    public int hours;
    public int minutes;

    private void TimerUpdate()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;

            if (timer >= 60)
            {
                timer -= 60;
                minutes++;
            }

            if (minutes >= 60)
            {
                minutes -= 60;
                hours++;
            }

        }
    }
    #endregion 
}
