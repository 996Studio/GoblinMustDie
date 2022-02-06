using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;
    
    public List<TowerBase> towerBaseList;
    private List<Node> nodeList;

    public List<Node> NodeList
    {
        get { return nodeList; }
    }

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
        nodeList = new List<Node>();

        Node[] nodes= FindObjectsOfType<Node>();
        foreach (var node in nodes)
        {
            nodeList.Add(node);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildTower(TowerType type,Node node)
    {
        Debug.Log(type);
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
            case TowerType.WOODEN:
            {
                node.Tower = Instantiate(towerBaseList[1].TowerPrefab,
                    node.transform.position + towerBaseList[1].TowerBuildOffset, Quaternion.identity);
                node.TowerType = TowerType.WOODEN;
                ChangeResource(-towerBaseList[1].CoinCost[0], -towerBaseList[1].WoodCost[0],
                    -towerBaseList[1].RockCost[0]);
                Debug.Log("Build wooden tower");
                break;
            }
            default: break;
        }
    }

    public void SellTower(Node node)
    {
        Debug.Log("Sell " + node.TowerType);
        switch (node.TowerType)
        {
            case TowerType.BASIC:
            {
                ChangeResource(towerBaseList[0].CoinCost[0], towerBaseList[0].WoodCost[0],
                    towerBaseList[0].RockCost[0]);
                break;
            }
            case TowerType.WOODEN:
            {
                ChangeResource(towerBaseList[1].CoinCost[0], towerBaseList[1].WoodCost[0],
                    towerBaseList[1].RockCost[0]);
                break;
            }
            default: break;
        }
        Destroy(node.Tower);
        node.TowerType = TowerType.NULL;
        
    }

    public void LoadTower(Node node, TowerType type, int level)
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
            case TowerType.WOODEN:
            {
                node.Tower = Instantiate(towerBaseList[1].TowerPrefab,
                    node.transform.position + towerBaseList[1].TowerBuildOffset, Quaternion.identity);
                node.TowerType = TowerType.BASIC;
                node.Tower.GetComponent<ResourceTower>().Level = level;
                Debug.Log("Load wooden tower");
                break;
            }
            default: break;
        }
    }

    public void ChangeResource(int coin = 0, int wood = 0, int rock = 0)
    {
        ResourceManager.Instance().ChangeCoin(coin);
        ResourceManager.Instance().ChangeWood(wood);
        ResourceManager.Instance().ChangeRock(rock);
    }
}
