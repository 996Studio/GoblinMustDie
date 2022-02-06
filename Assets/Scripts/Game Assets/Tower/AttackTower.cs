using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackTower : BaseTower
{
    [Header("Attributes")] 
    protected float attackRange = 10.0f;
    protected float attack;
    protected float defence;
    [SerializeField] protected GameObject bulletPrefab;
    protected float fireRate = 1.0f;
    [SerializeField] protected Transform fireLocation;
    [SerializeField] protected Transform Rotator;
    protected float turnSpeed = 10.0f;
    

    protected float fireCounter;
    private Transform target;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

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

        turretAimActivate();

        // Rate of fire
        if (fireCounter <= 0)
        {
            turretFire();
            fireCounter = 1f / fireRate;
        }
        fireCounter -= Time.deltaTime;
    }

    protected void turretFire()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, fireLocation.position, fireLocation.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        FindObjectOfType<AudioManager>().Play("BowTowerFire");//BowTower fire sound

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    protected void UpdateTarget()
    {
        // Array of all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

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
        if (nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    // Smooth turn towards target enemy
    protected void turretAimActivate()
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
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
