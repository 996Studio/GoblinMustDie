using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;
    
    public List<TowerBase> towerBaseList;
    private List<NewNode> nodeList;

    public NodeManager Instance()
    {
        if (instance == null)
        {
            instance = this;
        }

        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        nodeList = new List<NewNode>();

        NewNode[] nodes= FindObjectsOfType<NewNode>();
        foreach (var node in nodes)
        {
            nodeList.Add(node);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildTower(TowerType type,NewNode node)
    {
        if (ResourceManager.Instance().Coin < towerBaseList[(int)type - 1].CoinCost[0] ||
            ResourceManager.Instance().Wood < towerBaseList[(int)type - 1].WoodCost[0] ||
            ResourceManager.Instance().Rock < towerBaseList[(int)type - 1].RockCost[0])
        {
            Debug.Log("need more coin");
            return;
        }
        
        Debug.Log("Build tower");
        switch (type)
        {
            case TowerType.BASIC:
            {
               node.Tower = Instantiate(towerBaseList[0].TowerPrefab,
                    node.transform.position + towerBaseList[0].TowerBuildOffset, Quaternion.identity);
               node.TowerType = TowerType.BASIC;
               ChangeResource(-towerBaseList[0].CoinCost[0], -towerBaseList[0].WoodCost[0],
                   -towerBaseList[0].RockCost[0]);
                Debug.Log("Build archer");
                break;
            }
        }
    }

    public void SellTower(NewNode node)
    {
        Debug.Log("Sell tower");
        Destroy(node.Tower);
        node.TowerType = TowerType.NULL;
        ChangeResource(towerBaseList[0].CoinGet[0], towerBaseList[0].WoodGet[0],
            towerBaseList[0].RockGet[0]);
    }

    public void LoadTower(NewNode node, TowerType type, int level)
    {
        switch (type)
        {
            case TowerType.BASIC:
            {
                node.Tower = Instantiate(towerBaseList[0].TowerPrefab,
                    node.transform.position + towerBaseList[0].TowerBuildOffset, Quaternion.identity);
                node.TowerType = TowerType.BASIC;
                node.Tower.GetComponent<ArcherTower>().Level = level;
                Debug.Log("Load archer");
                break;
            }
        }
    }

    public void ChangeResource(int coin = 0, int wood = 0, int rock = 0)
    {
        ResourceManager.Instance().ChangeCoin(coin);
        ResourceManager.Instance().ChangeWood(wood);
        ResourceManager.Instance().ChangeRock(rock);
    }
}
