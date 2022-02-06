using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeList : MonoBehaviour
{
    public List<Node> nodes;
    public List<int> Lv1 = new List<int>();

    public BuildManager buildManager;
    
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //print(Lv1[0]);
        }
    }

    public void SaveData()
    {
        Lv1.Clear();

        foreach (Node _node in nodes)
        {
            Lv1.Add(_node.towerIndex);
            Debug.Log(_node + " --- " + _node.towerIndex.ToString());
            
        }
    }

    public void LoadData()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (Lv1[i] == 1)
            {
                buildManager.BuildTower2(nodes[i]);
            }
        }
    }

    public void rebuildLevel()
    {
        foreach (Node _node in nodes)
        {
            

        }
    }
}

