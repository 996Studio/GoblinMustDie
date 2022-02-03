// Source filename : Resource.cs
// This is a script that controls the behaviour of each individual nodes 
// Version 0.1 By Jing on 2022/1/27

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTower : Turret
{
    [Header("Resource Tower")]
    public ResourceType resourcetoSpawn;
    public GameObject resourceSpawnPoint;
    public GameObject woodPrefab;
    public GameObject crytalPrefab;
    public int timeToSpawn;
    public bool bResourceIsUp;
    public float initialSpawnTime = 0.0f;
    public Vector3 resourceOffset = new Vector3(0,2,0);
    
    // Start is called before the first frame update
    void Start()
    {
        bResourceIsUp = false;
        woodPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        spawnResouce();
    }


    public void spawnResouce()
    {
        if (bResourceIsUp) return;
        
        if (Time.time>initialSpawnTime)
        {
            initialSpawnTime += timeToSpawn;
            Debug.Log("Spawn Resource");
            woodPrefab.SetActive(true);
            bResourceIsUp = true;
        }
    }

    
    public override void turretUpgrade()
    {
        throw new System.NotImplementedException();
    }

    public override void turretDestroy()
    {
        throw new System.NotImplementedException();
    }
}
