using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemoScriptUI : MonoBehaviour
{
    GameManager gameManager;
    WaveManager waveManager;

    public TMP_Text gameTimer;
    private string gameTimerText;

    public TMP_Text waveCount;
    private string waveCountText;

    private void Start()
    {
        gameManager = GameManager.Instance;
        waveManager = WaveManager.Instance;
    }
    private void Update()
    {
        gameTimerText = string.Format("{0:00}:{1:00}:{2:00}", gameManager.hours, gameManager.minutes, gameManager.timer);
        gameTimer.text = gameTimerText;

        waveCountText = "Wave " + (waveManager.waveIndex + 1);
        waveCount.text = waveCountText;
    }
}
