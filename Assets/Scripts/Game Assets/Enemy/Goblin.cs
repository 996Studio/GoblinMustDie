using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyBase,IHeal
{
    private double healFactor;
    
    public override void InitInfo()
    {
        base.InitInfo();
        
        maxHP = 100;
        curHP = 100;
        atk = 1;
        moveSpeed = 2.5f;
        
        agent.speed = moveSpeed;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
        
        healFactor = 0.2f;
    }

    protected override void Update()
    {
        base.Update();
    }

    public void heal()
    {
        if (!isDead && (curHP < maxHP))
        {
            curHP += maxHP * 0.2f;
            
            ChangeHealth();
            Debug.Log("Healed:" + maxHP * 0.2);
        }
    }
}
