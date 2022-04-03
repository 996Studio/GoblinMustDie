using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;

public class Fireball : Bullet
{
    public float explosionRange;
    public int explosionDamage;
    public float explosionElementAmount;
    public int explosionElementPower;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    
    protected override void HitTarget()
    {
        if (HitEffect != null)
        {
            GameObject effectInstance = (GameObject) Instantiate(HitEffect, transform.position, transform.rotation);
            Destroy(effectInstance, 2f);
        }

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
            enemy.ElementAttack(elementType, elementAmount, elementPower, attack);
            AudioManager.instance.Play(SoundType.SFX,"FireballHit");

            //AOE
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
            foreach (Collider nearbyEnemy in colliders)
            {
                //Debug.Log(nearbyEnemy);
                EnemyBase tempEnemy = nearbyEnemy.GetComponent<EnemyBase>();
                if (tempEnemy != null)
                {
                    tempEnemy.ElementAttack(elementType, explosionElementAmount, explosionElementPower, explosionDamage);
                    //Debug.Log("AOE to " + target);
                }
            }
        }

        isUsed = true;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
