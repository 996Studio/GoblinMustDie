using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : Resource
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationSpeed * Time.deltaTime);
    }

    public override void CollectResource()
    {
        ResourceManager.Instance().Wood += ResourceAmount;
        Debug.Log("You have: " + ResourceManager.Instance().Wood + " wood.");
        base.CollectResource();
    }
}
