using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable] 
public class FireTower : AttackTower
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

        // Rate of fire
        if (fireCounter <= 0)
        {
            ShootBullet();
            fireCounter = fireInterval;
            AudioManager.instance.Play(SoundType.SFX,"ShootFireball");
        }
        fireCounter -= Time.deltaTime;
    }
}