using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MotherBase : MonoBehaviour
{
    private int curHP;
    private int maxHP;

    private bool isDead = false;
    private bool isWin = false;
    public int killCount = 0; //temp value to be modified later
    
    private static MotherBase instance;
    public static MotherBase Instance => instance;
    
    public int CurHp
    {
        get => curHP;
        set => curHP = value;
    }
    
    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }

    public bool IsWin
    {
        get => isWin;
        set => isWin = value;
    }
    
    void Awake()
    {
        instance = this;
        curHP = 10;
        maxHP = 10;
    }

    private void Start()
    {
        HUDManager.instance.UpdateLiveText(this.curHP);
    }

    private void Update()
    {
        if (killCount == 10 && !isDead)
            isWin = true;

        if (isDead)
        {
            SceneManager.LoadScene("WinScene 1");
            GameObject.Find("LevelData").GetComponent<LevelData>().isWin = false;
            GameObject.Find("LevelData").GetComponent<LevelData>().killCount = killCount;
            Debug.Log(
                $"Mother Win {GameObject.Find("LevelData").GetComponent<LevelData>().isWin}, kill {GameObject.Find("LevelData").GetComponent<LevelData>().killCount}");
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
        }
        else if (isWin)
        {
            SceneManager.LoadScene("WinScene 1");
            GameObject.Find("LevelData").GetComponent<LevelData>().isWin = true;
            GameObject.Find("LevelData").GetComponent<LevelData>().killCount = killCount;
            SceneManager.LoadScene("WinScene");
        }
            
    }

    public void UpdateHp(int hp, int maxHp)
    {
        this.curHP = hp;
        this.maxHP = maxHp;
        
        //Update UI info later
        HUDManager.instance.UpdateLiveText(this.curHP);
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
            TakeDamage(other.gameObject.GetComponent<EnemyBase>().Atk);
            other.gameObject.GetComponent<EnemyBase>().Death();
            //Debug.Log(curHP);
        }
    }
}
