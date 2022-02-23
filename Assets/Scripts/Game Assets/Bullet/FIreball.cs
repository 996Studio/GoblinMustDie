using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Bullet
{
    public float explosionRange;
    public int explosionDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected override void HitTarget()
    {
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);
        
        if (target != null)
        {
            
            Destroy(gameObject, SecondsBeforeDestroy);
        }
        else
        {
            Destroy(gameObject);
        }

        EnemyBase enemy = target.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(attack);
            
            //AOE
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
            foreach (Collider nearbyEnemy in colliders)
            {
                EnemyBase tempEnemy = nearbyEnemy.GetComponent<EnemyBase>();
                if (tempEnemy != null)
                {
                    tempEnemy.TakeDamage(explosionDamage);
                }
            }
        }
    }
}
