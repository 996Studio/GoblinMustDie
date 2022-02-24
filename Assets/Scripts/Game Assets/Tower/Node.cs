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

        //CreateTowerUI.menuCall += showUI;

        if (towerType != TowerType.NULL)
        {
            NodeManager.instance.InstantiateTower(this, towerType, 1);
        }
    }

    //private void OnDisable()
    //{
    //    CreateTowerUI.menuCall -= showUI;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void showUI()
    //{
    //    print("Menu called");
    //}

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //CreateTowerUI.instance.hidePanel();
            Debug.Log("Nothing here");
            return;
        }
        
        Debug.Log("On mouse down");
        if (nodeType == NodeType.PATH)
        {
            CreateTowerUI.instance.hidePanel();
            Debug.Log("Path return");
            return;
        }

        if (tower != null && towerType != TowerType.NULL && towerType != TowerType.WOODEN)
        {
            // For now, if a turret exist at this node, remove the turret.
            //NodeManager.instance.SellTower(this, this.Tower.GetComponent<BaseTower>().Level);

            CreateTowerUI.instance.showPanel();
            CreateTowerUI.instance.selectNode = this;
            return;
        }
        
        // Build a turret
        if (tower == null && towerType == TowerType.NULL)
        {
            CreateTowerUI.instance.showPanel();
            CreateTowerUI.instance.selectNode = this;

            //NodeManager.instance.BuildTower(TowerType.ARCHER, this);
            //AudioManager.instance.Play(SoundType.SFX,"BowTowerBuild");
        }
    }

    // Keyboard input for testing
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (nodeType == NodeType.PATH)
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
        if (nodeType == NodeType.PATH)
        {
            return;
        }
        rend.material.color = originColor;
    }
}