using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower : AttackTower
{
    public GameObject attackCollider;

    private bool isAttacking = false;
    public float spreadTime;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        // Invoke target function, start from 0s, and repeat every 0.5s.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        attackCollider.GetComponent<WaterRing>().Damage = attack;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        
        if (target == null) return; // No target, do nothing

        // Rate of fire
        if (fireCounter <= 0)
        {
            RingAttackStart();
            fireCounter = fireInterval;
        }
        fireCounter -= Time.deltaTime;

        if (isAttacking)
        {
            attackCollider.transform.localScale +=
                new Vector3(1.0f, 1.0f, 1.0f) * (attackRange / spreadTime) * Time.deltaTime;
            if (attackCollider.transform.localScale.x >= attackRange)
            {
                RingAttackStop();
            }
        }
    }

    private void RingAttackStart()
    {
        attackCollider.SetActive(true);
        attackCollider.transform.localScale=Vector3.zero;
        isAttacking = true;
    }

    private void RingAttackStop()
    {
        attackCollider.SetActive(false);
        isAttacking = false;
    }
}
