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
    private float waveInterval = 3;

    private float waveIntervalCounter;
    
    int currentWave = 0;
    int spawnNum = 0;    
    float spawnInverval = 0;
    float spawnCounter = 0;

    private bool isSpawnEnd = false;
    
    private void Start()
    {
        waveIntervalCounter = waveInterval;
        spawnCounter = enemySpawnerSOs[currentWave].spawnInterval;
    }

    private void Update()
    {
        waveIntervalCounter -= Time.deltaTime;

        if (waveIntervalCounter <= 0)
        {
            spawnCounter -= Time.deltaTime;
            Debug.Log("spawn counter: " + spawnCounter);
            
            if (spawnCounter <= 0 && spawnNum < enemySpawnerSOs[currentWave].spawnAmount)
            {
                GameObject tempGo = Instantiate(enemySpawnerSOs[currentWave].enemy, transform.position, Quaternion.identity, transform);
                GameManager.Instance.spawnedEnemies.Add(tempGo);
                spawnNum++;

                spawnCounter = spawnInverval;
            }

            if (spawnNum >= enemySpawnerSOs[currentWave].spawnAmount && currentWave + 1 < enemySpawnerSOs.Length)
            {
                currentWave++;
                spawnNum = 0;
                
                spawnInverval = enemySpawnerSOs[currentWave].spawnInterval;
                spawnCounter = spawnInverval;

                waveIntervalCounter = waveInterval;
            }

            if (spawnNum >= enemySpawnerSOs[currentWave].spawnAmount && currentWave + 1 >= enemySpawnerSOs.Length)
            {
                isSpawnEnd = true;
            }
        }
        
        Debug.Log("isSpawnEnd: " + isSpawnEnd);
    }
}
