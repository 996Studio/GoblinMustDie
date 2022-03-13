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
        moveSpeed = 0.5f;
        
        agent.speed = moveSpeed;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
        
        healFactor = 0.2f;
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log("troll health: " + curHP);
    }

    public override void Death()
    {
        Debug.Log("Troll death");
        isDead = true;
        agent.isStopped = true;
        
        AudioManager.instance.Play(SoundType.SFX, "EnemyDeath");
        
        Instantiate(goblinPre, transform.position, Quaternion.identity, transform);
        Instantiate(goblinPre, transform.position, Quaternion.identity, transform);
        
        Destroy(this.gameObject);
    }

    public void heal()
    {
        if (!isDead)
        {
            curHP += maxHP * 0.2f;
        }
    }
}
