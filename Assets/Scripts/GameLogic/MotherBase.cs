 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MotherBase : MonoBehaviour
{
    public static MotherBase Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.TakeDamage(other.gameObject.GetComponent<EnemyBase>().Atk);
            other.gameObject.GetComponent<EnemyBase>().Death();
        }
    }
}
