using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackTower : BaseTower
{
    [Header("Attributes")] 
    protected float attackRange = 10.0f;
    protected int attack;
    protected float defence;
    protected float fireRate = 1.0f;
    protected float turnSpeed = 10.0f;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform fireLocation;
    [SerializeField] protected Transform Rotator;

    protected float fireCounter;
    protected Transform target;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    // Start is called before the first frame update
     protected  void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected void ShootBullet()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, fireLocation.position, fireLocation.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.Attack = this.attack;
        AudioManager.instance.Play(SoundType.SFX,"BowTowerFire");

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
            Debug.Log(target);
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
    
    public void SetAttackTowerData(TowerBase towerData, int level)
    {
        attack = towerData.AttackValue[level - 1];
        attackRange = towerData.AttackRange[level - 1];
        fireRate = towerData.AttackInterval[level - 1];
    }
}
