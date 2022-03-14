using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class Troll : EnemyBase,IHeal
{
    private double healFactor;

    [SerializeField]
    private GameObject goblinPre;
    public override void InitInfo()
    {
        base.InitInfo();
        
        maxHP = 200;
        curHP = 200;
        atk = 1;
        moveSpeed = 0.8f;
        coinValue = 500;
        
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
        Instantiate(goblinPre, transform.position, Quaternion.identity);
        GameManager.Instance.SpawnNum++;
        
        Instantiate(goblinPre, transform.position, Quaternion.identity);
        GameManager.Instance.SpawnNum++;
        
        Invoke("DeathEvent", 0.5f);
    }

    private void DeathEvent()
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
