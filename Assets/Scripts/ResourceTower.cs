// Source filename : Resource.cs
// This is a script that controls the behaviour of each individual nodes 
// Version 0.1 By Jing on 2022/1/27

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTower : Turret
{
    [Header("Resource Tower")]
    public int timeToSpawn;
    public ResourceType resourcetoSpawn;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnResouce()
    {
        
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
