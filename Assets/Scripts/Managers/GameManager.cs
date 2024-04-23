using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Menu,
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameStates currentState;

    public int killCount;

    public int playerScore;

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
        if (currentState == GameStates.Playing)
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.LevelStartSetup();
            }
        }
    }

    private void Update()
    {
        GameStateManager();
    }


    private void GameStateManager()
    {
        if (currentState == GameStates.Playing)
        {
            TimerUpdate();
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    
    //------------------------------------------------------------------------------------
    //                                      Game Timer
    //------------------------------------------------------------------------------------
    #region Game Timer
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
