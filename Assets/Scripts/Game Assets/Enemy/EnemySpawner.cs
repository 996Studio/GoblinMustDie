using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] 
    private EnemySpawnerSO [] enemySpawnerSOs;

    [SerializeField] 
    private float waveInterval = 5;

    private float waveIntervalCounter;
    
    int currentWave = 0;
    int spawnNum = 0;    
    float spawnInterval = 0;
    float spawnCounter = 0;
    
    private void Start()
    {
        waveIntervalCounter = waveInterval;
        spawnInterval = enemySpawnerSOs[currentWave].spawnInterval;
        spawnCounter = spawnInterval;
    }

    private void Update()
    {
        waveIntervalCounter -= Time.deltaTime;

        if (waveIntervalCounter <= 0)
        {
            spawnCounter -= Time.deltaTime;

            if (spawnCounter <= 0 && spawnNum < enemySpawnerSOs[currentWave].spawnAmount)
            {
                GameObject tempGo = Instantiate(enemySpawnerSOs[currentWave].enemy, transform.position, Quaternion.identity, transform);
                GameManager.Instance.SpawnNum++;
                spawnNum++;
                
                spawnCounter = spawnInterval;
            }

            if (spawnNum >= enemySpawnerSOs[currentWave].spawnAmount && currentWave + 1 < enemySpawnerSOs.Length)
            {
                currentWave++;
                spawnNum = 0;
                
                spawnInterval = enemySpawnerSOs[currentWave].spawnInterval;
                spawnCounter = spawnInterval;

                waveIntervalCounter = waveInterval;
            }

            if (spawnNum >= enemySpawnerSOs[currentWave].spawnAmount && currentWave + 1 >= enemySpawnerSOs.Length)
            {
                GameManager.Instance.IsSpawnEnd = true;
            }
        }
    }
}
