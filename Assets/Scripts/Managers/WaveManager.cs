using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    public float multiplier;

    public int enemyCount;
    public int bossCount;
}

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;

    public float spawnDelay = 1f;

    public int currentEnemyCount;
    public int currentBossCount;

    public int waveIndex = 0;
    [SerializeField] private int nextWaveMin;
    public float spawnDistance;

    private void Start()
    {
        StartCoroutine(SpawnWave());
        nextWaveMin = GameManager.Instance.minutes + 1;
    }
    private void Update()
    {
        WaveTimer();
    }

    public void WaveTimer()
    {
        if (GameManager.Instance.minutes == nextWaveMin)    //  if the timer reaches a minute the wave progresses to the next wave
        {
            nextWaveMin++;
            waveIndex++;
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        Wave currentWave = waves[waveIndex];
        if (currentWave.enemyCount != 0)
            if (currentWave.enemyPrefabs != null || currentWave.enemyPrefabs.Length != 0) 
                while (currentEnemyCount < currentWave.enemyCount)
                {
                    SpawnEnemy(currentWave.enemyPrefabs[UnityEngine.Random.Range(0, currentWave.enemyPrefabs.Length)],currentWave.multiplier, false);

                    yield return new WaitForSeconds(spawnDelay);
                }
        if (currentWave.bossCount != 0)
            while (currentBossCount < currentWave.bossCount)
            {
                SpawnEnemy(currentWave.bossPrefab, currentWave.multiplier, true);
            }
        if (waveIndex == currentWave.enemyPrefabs.Length - 1) GameManager.Instance.isFinalWave = true;
        else GameManager.Instance.isFinalWave = true;
    }

    //  Used to Spawn Enemy/Boss 
    private void SpawnEnemy(GameObject enemy, float currentMultiplier, bool isBoss)
    {
        Vector3 playerPos = FindFirstObjectByType<PlayerController>().transform.position;
        Vector3 spawnPos = new Vector3(playerPos.x + UnityEngine.Random.Range(-spawnDistance, spawnDistance), 3, playerPos.z + UnityEngine.Random.Range(-spawnDistance, spawnDistance));

        var enemyClone = Instantiate(enemy, spawnPos, Quaternion.identity);
        enemyClone.GetComponent<EnemyBehavior>().multiplier = currentMultiplier;
        enemyClone.GetComponent<EnemyBehavior>().spawnedFromBoss = false;
        if (!isBoss) currentEnemyCount++;
        else
        {
            currentBossCount++;
            enemyClone.GetComponent<BossEnemy>().spawnMultiplier = currentMultiplier;
        }
    }
}
