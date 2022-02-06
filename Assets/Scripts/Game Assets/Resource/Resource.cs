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

    public ResourceType resourceType;

    public delegate void GatherResourceAction(ResourceType type); 
    public static event GatherResourceAction gatherResourceEvent;
    
    
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
        CollectResource(resourceType, ResourceAmount);
    }
    public void CollectResource(ResourceType type, int amount)
    {
        this.gameObject.SetActive(false);
        switch (type)
        {
            case ResourceType.WOOD:
                ResourceManager.Instance().Wood += ResourceAmount;
                Debug.Log("Wood: "+ ResourceManager.Instance().Wood);
                break;
            case ResourceType.ROCK:
                break;
            default:
                break;
        }
        
        if (gatherResourceEvent != null)
        {
            gatherResourceEvent(type);
        }
        
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
