using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;

public class ThunderTower : AttackTower
{
    [Header("Thunder Tower Attributes")]
    public int chainNumber;
    public float chainRange;

    private ElementEnum elementType = ElementEnum.Thunder;
    public float elementAmount;
    public int elementPower;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        // Invoke target function, start from 0s, and repeat every 0.5s.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        
        if (target == null) return; // No target, do nothing

        // Rate of fire
        if (fireCounter <= 0)
        {
            Lightning();
            fireCounter = fireInterval;
        }
        fireCounter -= Time.deltaTime;
    }

    private void Lightning()
    {
        Debug.Log("Lightning!");
        Vector3 dir = target.position - fireLocation.position;
        Instantiate(bulletPrefab, fireLocation.position, Quaternion.LookRotation(dir));
        EnemyBase enemy = target.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.ElementAttack(elementType, elementAmount, elementPower, attack);
        }

        List<EnemyBase> enemyList = enemy.GetNearestEnemy(chainNumber, chainRange);
        if (enemyList.Count == 0)
        {
            return;
        }

        foreach (EnemyBase nearbyEnemy in enemyList)
        {
            Vector3 tempDir = nearbyEnemy.transform.position - target.position;
            Instantiate(bulletPrefab, nearbyEnemy.transform.position, Quaternion.LookRotation(dir));
            nearbyEnemy.ElementAttack(elementType, elementAmount, elementPower, attack);
            //Debug.Log($"收衣服拉{nearbyEnemy}");
        }
    }
}
