using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTower : BaseTower
{
    [Header("Resource Tower")]
    public ResourceType resourcetoSpawn;
    public GameObject resource;
    public int timeToSpawn;
    public bool bResourceIsUp;
    public float initialSpawnTime = 1.0f;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        bResourceIsUp = false;
        resource.SetActive(false);
        InvokeRepeating("spawnResouce",1,5);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }


    public void spawnResouce()
    {
        if (bResourceIsUp) return;
        //Debug.Log("Spawn Resource");
        resource.SetActive(true);
        bResourceIsUp = true;
    }
}
