using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : Bullet
{
    public float freezeDuration;
    public float freezeScale;

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
            enemy.TakeDamage(attack);
            enemy.StartFreeze(freezeScale, freezeDuration);
        }

        isUsed = true;
    }
}