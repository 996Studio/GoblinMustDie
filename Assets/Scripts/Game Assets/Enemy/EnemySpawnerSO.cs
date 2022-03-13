using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "EnemySpawner")]
public class EnemySpawnerSO : ScriptableObject
{
   public GameObject enemy;
   public int spawnAmount = 5;
   public float spawnInterval = 0.5f;
}


