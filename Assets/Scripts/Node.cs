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
    [Range(1,10)]
    public int numberOfRow = 5;
    [Range(1,10)]
    public int numberOfColum = 5;
    [Range(1, 4)] public float sizeOfNodes = 4.0f;
    [Range(1, 4)] public float distancebwNodes = 0.5f;

    
    [Header("NodeType")] 
    [SerializeField]
    NodeType type1;
    [SerializeField]
    TowerType type2;

    public Color hoverColor;

    private Renderer rend;
    private Color originColor;
    private GameObject turret;
    public Vector3 turretOffset;

    

    
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
            // For now, if a turret exist at this node, remove the turret.
            Destroy(turret);
            return;
        }

        // Build a turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + turretOffset, transform.rotation);
        FindObjectOfType<AudioManager>().Play("BowTowerBuild");//Build Sound
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

