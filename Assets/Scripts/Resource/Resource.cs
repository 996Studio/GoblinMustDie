using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.Oculus.Input;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public Vector3 RotationSpeed;

    public int ResourceAmount;
    
    public Color hoverColor;

    protected Renderer rend;

    protected Color originColor;

    protected ResourceTower tower;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        originColor = rend.material.color;
        tower = GetComponentInParent<ResourceTower>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public virtual void OnMouseDown()
    {
        Debug.Log("Clicked");
        CollectResource();
    }
    public virtual void CollectResource()
    {
        this.gameObject.SetActive(false);
        tower.bResourceIsUp = false;
    }
    public virtual void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    public virtual void OnMouseExit()
    {
        rend.material.color = originColor;
    }
}
