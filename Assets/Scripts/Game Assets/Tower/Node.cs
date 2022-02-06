using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("NodeType")]
    [SerializeField] NodeType nodeType;
    [SerializeField] private TowerType towerType = TowerType.NULL;

    public Color hoverColor;

    private Renderer rend;
    private Color originColor;
    private GameObject tower;
    

    public GameObject Tower
    {
        get { return tower;}
        set { tower = value; }
    }

    public TowerType TowerType
    {
        get { return towerType; }
        set { towerType = value; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        originColor = rend.material.color;

        if (towerType != TowerType.NULL)
        {
            NodeManager.instance.BuildTower(towerType, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        Debug.Log("On mouse down");
        if (nodeType == NodeType.PATH)
        {
            Debug.Log("Path return");
            return;
        }

        if (tower != null && towerType != TowerType.NULL && towerType != TowerType.WOODEN)
        {
            // For now, if a turret exist at this node, remove the turret.
            NodeManager.instance.SellTower(this);
            return;
        }
        
        // Build a turret
        if (tower == null && towerType == TowerType.NULL)
        {
            NodeManager.instance.BuildTower(TowerType.BASIC, this);
            AudioManager.instance.Play(SoundType.SFX,"BowTowerBuild");
        }
    }

    // Keyboard input for testing
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        rend.material.color = originColor;
    }
}