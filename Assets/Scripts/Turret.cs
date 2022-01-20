using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    private int turretLevel = 1;

    public Transform target;
    public float range = 1f;
    public string enemyTag = "Enemy";
    public Transform Rotator;
    public float turnSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public abstract void turretUpgrade();
    public abstract void turretDestroy();



}
