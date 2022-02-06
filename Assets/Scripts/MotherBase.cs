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
    
    public int CurHp
    {
        get => curHP;
        set => curHP = value;
    }
    
    void Awake()
    {
        instance = this;
        curHP = 300;
        maxHP = 300;
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
            Debug.Log("Destroy");
            TakeDamage(other.gameObject.GetComponent<EnemyBase>().Atk);
            other.gameObject.GetComponent<EnemyBase>().Death();
            //Debug.Log(curHP);
        }
    }
}
