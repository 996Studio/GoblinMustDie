// Source filename : Node.cs
// This is a script that controls the behaviour of each individual nodes 
// Version 0.1 By Jing on 2022/1/16
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
}

