using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class Troll : EnemyBase,IHeal
{
    private double healFactor;
    private int spawnNum = 2;
    
    [SerializeField]
    private GameObject goblinPre;

    protected override void Awake()
    {
        base.Awake();
        enemyType = EnemyType.TROLL;
    }

    public override void InitInfo()
    {
        base.InitInfo();
        
        maxHP = 200;
        curHP = 200;
        atk = 1;
        moveSpeed = 1.75f;
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
        
        // for (int i = 0; i < spawnNum; i++)
        // {
        //     GameObject tempGoblin = Instantiate(goblinPre, transform.position, Quaternion.identity);
        //     tempGoblin.GetComponent<Goblin>().GetNearestWaypoint();
        //     tempGoblin.GetComponent<Goblin>().SetDestination();
        // }
        
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
