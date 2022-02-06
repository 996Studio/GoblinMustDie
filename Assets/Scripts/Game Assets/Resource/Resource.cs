using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resource : MonoBehaviour
{
    public Vector3 RotationSpeed;

    public int ResourceAmount;
    
    public Color hoverColor;

    protected Renderer rend;

    protected Color originColor;

    protected ResourceTower tower;

    public ResourceType resourceType;


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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        Debug.Log("Clicked");
        CollectResource(resourceType, ResourceAmount);
    }
    
    public void CollectResource(ResourceType type, int amount)
    {
        Debug.Log($"Collect {type} {amount}");
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
        
        HUDManager.instance.UpdateResourceText(type);

        tower.bResourceIsUp = false;
    }
    
    public virtual void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        rend.material.color = hoverColor;
    }
    public virtual void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        rend.material.color = originColor;
    }
}
