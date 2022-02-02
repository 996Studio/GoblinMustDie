using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : Resource
{
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        originColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationSpeed * Time.deltaTime);
    }

    public override void CollectResource()
    {
        ResourceManager.Instance().Wood += ResourceAmount;
        Debug.Log("Current Wood:" + ResourceManager.Instance().Wood);
    }

    public void OnMouseDown()
    {
        Debug.Log("Clicked");
        CollectResource();
    }
    
    /*public override void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    public override void OnMouseExit()
    {
        rend.material.color = originColor;
    }*/
    
    public void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    public void OnMouseExit()
    {
        rend.material.color = originColor;
    }

    
}
