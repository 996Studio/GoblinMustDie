using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("On mouse down");
        if (nodeType == NodeType.PATH)
        {
            Debug.Log("Path return");
            return;
        }

        if (tower != null && towerType != TowerType.NULL)
        {
            // For now, if a turret exist at this node, remove the turret.
            NodeManager.instance.SellTower(this);
            return;
        }

        FindObjectOfType<AudioManager>().Play("BowTowerBuild");
        // Build a turret
        NodeManager.instance.BuildTower(TowerType.BASIC, this);
    }

    // Keyboard input for testing
    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        rend.material.color = originColor;
    }
}