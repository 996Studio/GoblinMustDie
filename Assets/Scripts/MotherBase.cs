using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherBase : MonoBehaviour
{
    private int curHP;
    private int maxHP;
    private bool isDead;

    private static MotherBase instance;
    public static MotherBase Instance => instance;
    
    void Awake()
    {
        instance = this;
        curHP = 100;
        maxHP = 100;
    }
    
    public void UpdateHp(int hp, int maxHp)
    {
        this.curHP = hp;
        this.maxHP = maxHp;
        
        //Update UI info later
    }

    public void TakeDamage(int dmg)
    {
        if (isDead)
            return;
        
        curHP -= dmg;

        if (curHP <= 0)
        {
            curHP = 0;
            isDead = true;
        }
        
        UpdateHp(curHP, maxHP);
    }

    private void OnDestroy()
    {
        instance = null;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Console.WriteLine("Death");
            
            TakeDamage(other.gameObject.GetComponent<EnemyBase>().Atk);
            other.gameObject.GetComponent<EnemyBase>().Death();
        }
    }
}
