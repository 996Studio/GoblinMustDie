using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable] 
public class ArcherTower : AttackTower
{
    private void Start()
    {
        base.Start();
        
        // Invoke target function, start from 0s, and repeat every 0.5s.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        base.Update();
        
        if (target == null) return; // No target, do nothing

        turretAimActivate();

        // Rate of fire
        if (fireCounter <= 0)
        {
            ShootBullet();
            fireCounter = fireInterval;
        }
        fireCounter -= Time.deltaTime;
    }
}
