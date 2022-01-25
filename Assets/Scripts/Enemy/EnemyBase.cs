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
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
