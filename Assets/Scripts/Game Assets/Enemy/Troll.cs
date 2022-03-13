using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : EnemyBase,IHeal
{
    [SerializeField]
    private double healFactor;
    
    public override void InitInfo()
    {
        base.InitInfo();
        
        maxHP = 200;
        curHP = 200;
        atk = 1;
        moveSpeed = 1.0f;
        
        agent.speed = moveSpeed;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
        
        healFactor = 0.2f;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Death()
    {
        
        base.Death();
    }

    public void heal()
    {
        if (!isDead)
        {
            curHP += maxHP * 0.2f;
        }
    }
}
