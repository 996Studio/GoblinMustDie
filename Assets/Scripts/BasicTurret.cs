using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1. find target
//2. Rotate to target

public class BasicTurret : MonoBehaviour
{
    public Transform target;
    public float range = 1f;
    public string enemyTag = "Enemy";
    public Transform Rotator;
    public float turnSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        // Invoke target function, start from 0s, and repeat every 0.5s.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return; // No target, do nothing

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Rotator.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);

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

    void turretActivate()
    {

    }

    // Debug purpose: to show the turret range.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
