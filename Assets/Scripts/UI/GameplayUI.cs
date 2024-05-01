using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    GameManager gameManager;
    WaveManager waveManager;

    public GameObject gameplayPanel;
    public GameObject endGamePanel;

    //  Gameplay UI
    public TMP_Text scoreTxt;
    public TMP_Text timerTxt;
    public TMP_Text waveCountTxt;
    public TMP_Text killCountTxt;

    private string scoreString;
    private string gameTimerString;
    private string waveCountString;
    private string killCountlString;

    //  End Game UI
    public TMP_Text endGameTxt;
    public TMP_Text endGameScoreTxt;
    public TMP_Text endGameTimerTxt;
    public TMP_Text endGameWaveCountTxt;
    public TMP_Text endGameKillCountTxt;

    private void Start()
    {
        gameManager = GameManager.Instance;
        waveManager = FindFirstObjectByType<WaveManager>();
    }
    public void GameplayUIUpdate()
    {
        scoreString = "Score: " + gameManager.playerScore;
        gameTimerString = string.Format("{0:00}:{1:00}:{2:00}", gameManager.hours, gameManager.minutes, gameManager.timer);
        waveCountString = "Wave: " + (waveManager.waveIndex + 1);
        killCountlString = "Kills: " + gameManager.killCount;

        timerTxt.text = gameTimerString;
        scoreTxt.text = scoreString;
        waveCountTxt.text = waveCountString;
        killCountTxt.text = killCountlString;
    }
    public void EndGameText(bool isDead)
    {
        string endGame;
        if (isDead) endGame = "Game Over";
        else endGame = "You Win";
        endGameTxt.text = endGame;

        string endGameScore = scoreString;
        string endGameTimer = gameTimerString;
        string endGameWaves = waveCountString;
        string endGameKills = killCountlString;

        endGameScoreTxt.text = endGameScore;
        endGameTimerTxt.text = endGameTimer;
        endGameWaveCountTxt.text = endGameWaves;
        endGameKillCountTxt.text = endGameKills;
    }

    public void SwapUI(bool endGame) 
    {
        gameplayPanel.SetActive(!endGame);
        endGamePanel.SetActive(endGame);
    }

    private void Update()
    {
        GameplayUIUpdate();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
