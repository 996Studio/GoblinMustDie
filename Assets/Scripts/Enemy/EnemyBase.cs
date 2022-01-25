using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private int maxHP;
    private int curHP;
    
    public bool isDead = false;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    
    //Init monster Info
    //to be modified later by Jason data
    public void InitInfo()
    {
        maxHP = 100;
        curHP = 100;
        agent.speed = 3.5f;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
    }

    void Start()
    {
        //agent.SetDestination();
    }

    public void TakeDamage(int dmg)
    {
        curHP -= dmg;

        if (curHP <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;
        agent.isStopped = true;
        Destroy(this);
    }
    
    public void DeathEvent()
    {
        //Placeholder for animation event
    }

    void Update()
    {
        
    }
}
