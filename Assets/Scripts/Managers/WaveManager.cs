using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    [NonSerialized]public int enemyNum;
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;


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
    public float spawnDistance;

    public float timer;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }
    private void Update()
    {
        WaveTimer();
    }

    public void WaveTimer()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60f);  // Calculate minutes
        int seconds = Mathf.FloorToInt(timer % 60f);  // Calculate seconds

        Debug.Log(minutes);

        if (seconds == 0 && minutes > 0)
        {
            waveIndex++;
            StartCoroutine(SpawnWave());
            timer = 0f;

        }
    }

    IEnumerator SpawnWave()
    {
        Wave currentWave = waves[waveIndex];
        currentWave.enemyNum = UnityEngine.Random.Range(0, currentWave.enemyPrefabs.Length);
        if (currentWave.enemyCount != 0)
            while (currentEnemyCount < currentWave.enemyCount)
            {
                SpawnEnemy(currentWave.enemyPrefabs[currentWave.enemyNum], false);

                yield return new WaitForSeconds(spawnDelay);
            }
        if (currentWave.bossCount != 0)
            while (currentBossCount < currentWave.bossCount)
            {
                SpawnEnemy(currentWave.bossPrefab, true);

                yield return new WaitForSeconds(spawnDelay);
            }
    }

    //  Used to Spawn Enemy/Boss 
    private void SpawnEnemy(GameObject enemy, bool isBoss)
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 spawnPos = new Vector3(playerPos.x + UnityEngine.Random.Range(-spawnDistance, spawnDistance), 3, playerPos.z + UnityEngine.Random.Range(-spawnDistance, spawnDistance));

        Instantiate(enemy, spawnPos, Quaternion.identity);
        if (!isBoss) currentEnemyCount++;
        else currentBossCount++;
    }
}
