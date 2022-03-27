using System;
using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;

public class WaterRing : MonoBehaviour
{
    public Collider trigger;
    private int damage;
    public float elementAmount;
    public int elementPower;

    public int Damage
    {
        get => damage;
        set => damage = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().ElementAttack(ElementEnum.Water, elementAmount, elementPower, damage);
        }
    }
}
