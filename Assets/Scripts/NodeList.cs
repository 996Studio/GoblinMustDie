using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeList : MonoBehaviour
{
    public List<Node> nodes;
    public List<int> Lv1 = new List<int>();

    public BuildManager bm;
    
    //public int[] LevelOneNodeData = new int[33];
    public List<int> x;
    
    void Start()
    {
        //print(LevelOneNodeData.Length);
        
        for (int i = 0; i < 33; i++)
        {
            x.Add(1);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData(x);
        }
    }
    public void LoopThroughList()
    {
        //for (int i = 0; i < nodes.Count; i++)
        //{
        //    LevelOneNodeData[i] = nodes[i].towerIndex;
        //    Debug.Log("Node: " + i + "    Status: " + LevelOneNodeData[i]);
        //}

        //foreach (Node _node in nodes)
        //{
        //    Debug.Log(_node + "    " + _node.towerIndex.ToString());
        //    Lv1.Add(_node.towerIndex);
        //}
    }

    public void SaveData()
    {
        foreach (Node _node in nodes)
        {
            Debug.Log(_node + " --- " + _node.towerIndex.ToString());
            Lv1.Add(_node.towerIndex);
        }
    }

    public void LoadData(List<int> x)
    {
        for(int i = 0; i < x.Count; i++)
        {
            if (x[i] != 0)
            {
                bm.BuildTower();
            }
        }
    }
}

