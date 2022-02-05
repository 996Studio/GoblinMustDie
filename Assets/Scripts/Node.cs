// Source filename : Node.cs
// This is a script that controls the behaviour of each individual nodes 
// Version 0.1 By Jing on 2022/1/16
//         0.2 by Hancong on 2022/1/22 : Added mouse hover effect
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Nodes")]
    [Range(1, 10)]
    public int numberOfRow = 5;
    [Range(1, 10)]
    public int numberOfColum = 5;
    [Range(1, 4)] public float sizeOfNodes = 4.0f;
    [Range(1, 4)] public float distancebwNodes = 0.5f;


    [Header("NodeType")]
    [SerializeField]
    NodeType nodeType;
    [SerializeField]
    TowerType towerType;

    public Color hoverColor;

    private Renderer rend;
    private Color originColor;
    public GameObject turret;
    public Vector3 turretOffset;
    public BuildManager buildManager;

    public int towerIndex = 0;


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
        if (turret != null)
        {
            buildManager.RemoveTower(turret);
            towerIndex = 0;
            return;
        }

        // Build a turret
        buildManager.BuildTower2(this);
        turret = BuildManager.instance.thisTurret;
        towerIndex = 1;
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

    //private void BuildTower()
    //{
    //    //switch (towerIndex)
    //    //{
    //    //    case 0:
    //    //        break;
    //    //    case 1:
    //    //        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
    //    //        turret = (GameObject)Instantiate(turretToBuild, transform.position + turretOffset, transform.rotation);
    //    //        return;
    //    //    case 2:

    //    //    case 3:

    //    //    case 4:

    //    //    case 5:

    //    //    case 6:

    //    //    case 7:

    //    //    case 8:
    //    //}

    //    GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
    //    turret = (GameObject)Instantiate(turretToBuild, transform.position + turretOffset, transform.rotation);
    //    towerIndex = 1;
    //    ResourceManager.Instance().ChangeCoin(-10);
    //}

    //private void BuildTower(GameObject thisNode)
    //{
    //    GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
    //    turret = (GameObject)Instantiate(turretToBuild, thisNode.transform.position + turretOffset, thisNode.transform.rotation);
    //    towerIndex = 1;
    //    ResourceManager.Instance().ChangeCoin(-10);
    //}

}

