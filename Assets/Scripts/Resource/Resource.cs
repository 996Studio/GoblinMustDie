using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    public Vector3 RotationSpeed;

    public int ResourceAmount;
    
    public Color hoverColor;

    protected Renderer rend;

    protected Color originColor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        originColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void CollectResource();

    /*public abstract void OnMouseEnter();
    
    public abstract void OnMouseExit();*/
    
    /*public void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    public void OnMouseExit()
    {
        rend.material.color = originColor;
    }*/
}
