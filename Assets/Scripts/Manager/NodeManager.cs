using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;
    
    public List<TowerBase> towerBaseList;
    private List<Node> nodeList;

    private const int MAXTOWERLEVEL = 3;
    
    public List<Node> NodeList
    {
        get { return nodeList; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one NodeManager in scene!");
        }
        
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

    public void InstantiateTower(Node node,TowerType type,int level)
    {
        node.Tower = Instantiate(towerBaseList[(int)type - 1].TowerPrefab[level - 1],
            node.transform.position + towerBaseList[(int)type - 1].TowerBuildOffset, Quaternion.identity);
        node.TowerType = type;
        node.Tower.GetComponent<BaseTower>().Level = level;
        SetTowerData(node);
        node.Tower.transform.SetParent(node.transform);
    }

    public void BuildTower(TowerType type,Node node)
    {
        if (node.Tower != null)
        {
            return;
        }
        //Debug.Log(type);
        if (ResourceManager.Instance().Coin < towerBaseList[(int)type - 1].CoinCost[0] ||
            ResourceManager.Instance().Wood < towerBaseList[(int)type - 1].WoodCost[0] ||
            ResourceManager.Instance().Crystal < towerBaseList[(int)type - 1].RockCost[0])
        {
            //Debug.Log("need more coin");
            return;
        }

        //Debug.Log("Build tower");
        //Debug.Log($"type = {(int)type}");
        InstantiateTower(node, type, 1);
        ChangeResource(-towerBaseList[(int)type - 1].CoinCost[0], -towerBaseList[(int)type - 1].WoodCost[0],
            -towerBaseList[(int)type - 1].RockCost[0]);
        //Debug.Log($"Build {type}");
        AudioManager.instance.Play(SoundType.SFX, "TowerBuild");

        HUDManager.instance.UpdateResourceText(ResourceType.ALL);
    }

    public void UpgradeTower(Node node)
    {
        int level = node.Tower.GetComponent<BaseTower>().Level;
        if (level >= MAXTOWERLEVEL)
        {
            //Debug.Log("Tower reaches max level");
            return;
        }

        if (node.TowerType == TowerType.WOODEN || node.TowerType == TowerType.CRYSTAL)
        {
            //Debug.Log("Resource tower cannot be upgraded");
            return;
        }

        if (ResourceManager.Instance().Coin < towerBaseList[(int)node.TowerType - 1].CoinCost[level] ||
            ResourceManager.Instance().Wood < towerBaseList[(int)node.TowerType - 1].WoodCost[level] ||
            ResourceManager.Instance().Crystal < towerBaseList[(int)node.TowerType - 1].RockCost[level])
        {
            //Debug.Log("need more resource");
            return;
        }

        //Debug.Log("Upgrade tower level " + level);
        Destroy(node.Tower);
        InstantiateTower(node, node.TowerType, level + 1);
        AudioManager.instance.Play(SoundType.SFX, "TowerBuild");
        ChangeResource(-towerBaseList[(int)node.TowerType - 1].CoinCost[level],
            -towerBaseList[(int)node.TowerType - 1].WoodCost[level],
            -towerBaseList[(int)node.TowerType - 1].RockCost[level]);
        HUDManager.instance.UpdateResourceText(ResourceType.ALL);
    }

    public void SellTower(Node node)
    {
        Debug.Log("Sell " + node.TowerType);

        int level = node.Tower.GetComponent<BaseTower>().Level;
        ChangeResource(towerBaseList[(int)node.TowerType - 1].CoinCost[level - 1],
            towerBaseList[(int)node.TowerType - 1].WoodCost[level - 1],
            towerBaseList[(int)node.TowerType - 1].RockCost[level - 1]);
        HUDManager.instance.UpdateResourceText(ResourceType.ALL);
        
        Destroy(node.Tower);
        node.TowerType = TowerType.NULL;
        
    }

    public void LoadTower(Node node, TowerType type, int level)
    {
        if (node.TowerType == type)
        {
            if (node.Tower != null && node.Tower.GetComponent<BaseTower>().Level != level)
            {
                //do nothing
                //Debug.Log("Do nothing load tower");
            }
            else
            {
                //Debug.Log("load tower doesn't need to do anything");
                return;
            }
        }
        else if (node.Tower != null)
        {
            //Debug.Log("Destroy tower");
            Destroy(node.Tower);
            node.TowerType = TowerType.NULL;
        }

        InstantiateTower(node, type, level);
    }

    public void ChangeResource(int coin = 0, int wood = 0, int rock = 0)
    {
        ResourceManager.Instance().ChangeCoin(coin);
        ResourceManager.Instance().ChangeWood(wood);
        ResourceManager.Instance().ChangeCrystal(rock);
    }

    private void SetTowerData(Node node)
    {
        int level = node.Tower.GetComponent<BaseTower>().Level;
        
        switch (node.TowerType)
        {
            case TowerType.ARCHER:
            case TowerType.FIRE:
            case TowerType.ICE:
            case TowerType.WATER:
            case TowerType.THUNDER:
            {
                node.Tower.GetComponent<AttackTower>()
                    .SetAttackTowerData(towerBaseList[(int)node.TowerType - 1], level);
                break;
            }
            case TowerType.WOODEN:
            case TowerType.CRYSTAL:
            case TowerType.RECYCLE:
            {
                break;
            }
            default: break;
        }
    }
}
