using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeList : MonoBehaviour
{
    public List<Node> nodes;
    public int[] LevelOneNodeData = new int[33];
    //public Vector2[] nodeData;

    void Start()
    {
        print(LevelOneNodeData.Length);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoopThroughList();
        }
    }
    public void LoopThroughList()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            LevelOneNodeData[i] = nodes[i].towerIndex;
            Debug.Log("Node: " + i + "    Status: " + LevelOneNodeData[i]);
        }

    }
}

public struct NodeData
{
    
}
