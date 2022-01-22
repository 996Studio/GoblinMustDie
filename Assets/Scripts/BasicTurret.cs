using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. find target
// 2. Rotate to target

public class BasicTurret : Turret
{
    


    void Start()
    {
        // Invoke target function, start from 0s, and repeat every 0.5s.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null) return; // No target, do nothing

        turretAimActivate();

        // Rate of fire
        if (fireCountdown <= 0)
        {
            turretFire();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;


    }

    void turretFire()
    {
        Debug.Log("Fire!");
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void UpdateTarget()
    {
        // Array of all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // Store the shortest enemy to this turret
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Loop through all enemies
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // If this is the shortest distances found, set this to the nearest enemy target.
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Setting/Reset target
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    // Smooth turn towards target enemy
    void turretAimActivate()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Rotator.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    // Debug purpose: to show the turret range.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    public override void turretUpgrade()
    {
        
    }

    public override void turretDestroy()
    {
        
    }
}
