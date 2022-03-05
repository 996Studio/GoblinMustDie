using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingTower : MonoBehaviour
{
    public float recycleMultiplier;

    private SphereCollider detectSphere;
    
    // Start is called before the first frame update
    void Start()
    {
        detectSphere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().RecycleMultiplier = recycleMultiplier;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().RecycleMultiplier = 1.0f;
        }
    }
}
