using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkeletonMage : EnemyBase
{
    private double healInterval;
    private double healTimer;

    protected override void Update()
    {
        base.Update();
        
        if (!isDead)
            UpdateHealTimer();
    }

    public override void InitInfo()
    {
        base.InitInfo();
        
        maxHP = 60;
        curHP = 60;
        atk = 1;
        moveSpeed = 3.5f;
        coinValue = 350;
        
        agent.speed = moveSpeed;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;

        healInterval = 10;
        healTimer = 0;
    }

    private void UpdateHealTimer()
    {
        healTimer += Time.deltaTime;

        if (healTimer >= healInterval)
        {
            healTimer = 0;
            HealAbility();
        }
    }

    private void HealAbility()
    {
        List<IHeal> heals = new List<IHeal>();

        var healables = FindObjectsOfType<EnemyBase>().OfType<IHeal>();

        foreach (var enemy  in healables)
        {
            enemy.heal();
        }
    }
}
