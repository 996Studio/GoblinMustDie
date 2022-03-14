using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTower : BaseTower
{
    [Header("Resource Tower")]
    public ResourceType resourcetoSpawn;
    public GameObject woodPrefab;
    public GameObject crytalPrefab;
    public int timeToSpawn;
    public bool bResourceIsUp;
    public float initialSpawnTime = 1.0f;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        bResourceIsUp = false;
        woodPrefab.SetActive(false);
        InvokeRepeating("spawnResouce",1,5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnResouce()
    {
        if (bResourceIsUp) return;
        //Debug.Log("Spawn Resource");
        woodPrefab.SetActive(true);
        bResourceIsUp = true;
    }
}
