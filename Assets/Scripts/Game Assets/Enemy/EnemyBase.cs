using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    
    [SerializeField]
    protected NavMeshAgent agent;

    [SerializeField]
    protected double maxHP;
    
    [SerializeField]
    protected double curHP;
    
    [SerializeField]
    protected int atk;

    [SerializeField] protected float moveSpeed;
    
    protected bool isDead = false;
    protected bool canTakeDamage;
    
    //Freeze Parameter
    private float freezeCounter;
    
    public int Atk
    {
        get => atk;
    }
    
    public bool IsDead
    {
        get => isDead;
    }
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        agent.SetDestination(MotherBase.Instance.transform.position);
    }
    
    protected virtual void Update()
    {
        FreezeTimer();
    }

    public virtual void InitInfo()
    {
        atk = 1;
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
    
    //Placeholder for animation event
    // public void DeathEvent()
    // {
    //     
    // }

    private void FreezeTimer()
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

    public void StartFreeze(float freezeFactor, float duration)
    {
        agent.speed *= freezeFactor;
        freezeCounter = duration;
    }

    public void StopFreeze()
    {
        agent.speed = moveSpeed;
    }
}
