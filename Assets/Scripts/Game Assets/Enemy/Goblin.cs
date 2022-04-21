using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : EnemyBase,IHeal
{
    private double healFactor;
    private int nearestIndex = 0;
    protected override void Awake()
    {
        base.Awake();
        enemyType = EnemyType.GOBLIN;
    }

    public override void InitInfo()
    {
        base.InitInfo();
        
        maxHP = 100;
        curHP = 100;
        atk = 1;
        moveSpeed = 2.5f;
        coinValue = 200;
        
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
        }
    }

    public void OverrideDestination()
    {
        Debug.Log("Count: " + wayPoints.Count);
        int index = wayPoints.Count - 1;
        
        Debug.Log("Index: " + index);
        agent.SetDestination(wayPoints[index].position);
    }

    // public void GetNearestWaypoint()
    // {
    //     //Debug.Log("Nearest index: " + nearestIndex);
    //     Debug.Log("Count: " + wayPoints.Count);
    //     
    //     float minDis = Vector3.Distance(transform.position, wayPoints[0].position);
    //     
    //     for (int i = 1; i < wayPoints.Count; i++)
    //     {
    //         float nextDis = Vector3.Distance(transform.position, wayPoints[i].position);
    //         
    //         if (minDis > nextDis)
    //         {
    //             minDis = nextDis;
    //             nearestIndex = i;
    //         }
    //     }
    // }
}
