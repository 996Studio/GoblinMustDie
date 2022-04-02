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

    public Renderer rend;
    public Color originColor;
    private GameObject tower;
    public bool isSelect = false;
    private bool hover = false;

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

    // Update is called once per frame
    void Update()
    {
        
    }


#if UNITY_EDITOR
    //private void OnMouseDown()
    //{
    //    CreateTowerUI.instance.showPanel();
    //    CreateTowerUI.instance.selectNode = this;
    //}

    // Keyboard input for testing
    private void OnMouseEnter()
    {
        hover = true;
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
        hover = false;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (nodeType == NodeType.PATH)
        {
            return;
        }
        if (isSelect) return;
        rend.material.color = originColor;
    }
#endif
}