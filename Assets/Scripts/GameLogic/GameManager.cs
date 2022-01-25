using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
        
        int monterIndex = UnityEngine.Random.Range(0, 4);

        string type = string.Empty;

        switch (monterIndex)
        {
            case 0:
                type = "Goblin";
                break;
            case 1:
                type = "Bandits";
                break;
        }

        Pool.GetObject(type);
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
