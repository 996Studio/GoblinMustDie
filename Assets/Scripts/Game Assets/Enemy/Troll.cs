using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class Troll : EnemyBase,IHeal
{
    private double healFactor;
    private int spawnNum = 2;
    private List<GameObject> spawnedGoblins = new List<GameObject>();
    
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
        
        maxHP = 150;
        curHP = 150;
        atk = 1;
        moveSpeed = 1.50f;
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
        for (int i = 0; i < spawnNum; i++)
        {
            GameObject tempGoblin = Instantiate(goblinPre, transform.position, Quaternion.identity);
            GameManager.Instance.SpawnNum++;
            spawnedGoblins.Add(tempGoblin);
        }
        
        Invoke("DeathEvent", 0.5f);
    }

    private void DeathEvent()
    {
        for (int i = 0; i < spawnedGoblins.Count; i++)
        {
            spawnedGoblins[i].GetComponent<Goblin>().OverrideDestination();
        }
        
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
