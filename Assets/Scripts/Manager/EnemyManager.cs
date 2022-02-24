using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    private static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    
}
