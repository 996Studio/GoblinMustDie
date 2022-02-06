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
    private int atk;
    
    public int Atk
    {
        get => atk;
        set => atk = value;
    }

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
        atk = 10;
        agent.speed = 3.5f;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
    }

    void Start()
    {
        agent.SetDestination(MotherBase.Instance.transform.position);
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
        //FindObjectOfType<AudioManager>().Play("EnemyDeath");
        Destroy(this);
    }
    
    public void DeathEvent()
    {
        
        //Placeholder for animation event
    }
}
