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
    
    //Freeze Parameter
    private float startTimescale;
    private float startFixedDeltaTime;
    private float freezeCounter;
    
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
        atk = 1;
        agent.speed = 3.5f;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
    }

    void Start()
    {
        agent.SetDestination(MotherBase.Instance.transform.position);

        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (freezeCounter > 0.0f)
        {
            freezeCounter -= Time.deltaTime;
            if (freezeCounter <= 0)
            {
                freezeCounter = 0.0f;
                StopFreeze();
            }
        }
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
        
        AudioManager.instance.Play(SoundType.SFX, "EnemyDeath");
        
        Debug.Log(MotherBase.Instance.killCount);
        Destroy(this.gameObject);
    }
    
    public void DeathEvent()
    {
        
        //Placeholder for animation event
    }

    public void StartFreeze(float scale, float duration)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = startFixedDeltaTime * scale;
        freezeCounter = duration;
    }

    public void StopFreeze()
    {
        Time.timeScale = startTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }
}
