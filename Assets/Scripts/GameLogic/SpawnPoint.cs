using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnPoint : MonoBehaviour
{
    public int maxWave;
    public int enemyNumPerWave;
    private int curEnemyNum;

    public List<EnemyBase> enemySpawnList;
    private int curSpawnID;

    public float spawnInterval;
    public float waveInterval;
    public float firstWaveDelay;
    

    public int CurEnemyNum
    {
        get { return curEnemyNum; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartWave", firstWaveDelay);
    }

    private void StartWave()
    {
        curSpawnID = Random.Range(0, enemySpawnList.Count);
        curEnemyNum = enemyNumPerWave;

        HUDManager.instance.UpdateEnemyText(curEnemyNum);
        
        SpawnEnemy();

        --maxWave;
    }

    private void SpawnEnemy()
    {
        EnemyBase enemy = Instantiate(enemySpawnList[curSpawnID], transform.position, Quaternion.identity);
        enemy.InitInfo();

        curEnemyNum--;
        HUDManager.instance.UpdateEnemyText(curEnemyNum);

        if (curEnemyNum == 0)
        {
            if (maxWave > 0)
                Invoke("StartWave", waveInterval);
        }
        else
        {
            Invoke("SpawnEnemy", spawnInterval);
        }
    }

    //check if spawn is over
    public bool SpawnOver()
    {
        return curEnemyNum == 0 && maxWave == 0;
    }
    
    
}
