using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton BuildManager
    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager in scene!");
        }
        instance = this;
    }

    [Header("Tower Prefabs")]
    public GameObject standardTurretPrefab;
    public GameObject resourceTurretPrefab;

    public GameObject turretToBuild;

    public NodeList nodeList;
    public Vector3 turretOffset;
    public int towerIndex = 0;
    public int buildTowerIndex;

    public GameObject thisTurret;

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("--Selected Tower 1--");
            buildTowerIndex = 1;
            turretToBuild = standardTurretPrefab;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("--Selected Tower 5--");
            buildTowerIndex = 5;
            turretToBuild = resourceTurretPrefab;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            RemoveAllTower();
            RebuildLevel();
        }
        
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void RemoveTower(GameObject tower)
    {
        Destroy(tower);
        towerIndex = 0;
        ResourceManager.Instance().ChangeCoin(5);
    }

    //public void BuildTower()
    //{
    //    GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
    //    GameObject turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
    //    towerIndex = 1;
    //    ResourceManager.Instance().ChangeCoin(-10);
    //}

    public void BuildTower2(Node thisNode)
    {
        switch (buildTowerIndex)
        {
            case 1:
                WhichTowerToBuild(buildTowerIndex, thisNode);
                ResourceManager.Instance().ChangeCoin(-10);
                break;
            case 5:
                WhichTowerToBuild(buildTowerIndex, thisNode);
                ResourceManager.Instance().ChangeCoin(-30);
                break;
        }
    }

    private void WhichTowerToBuild(int selectedTower, Node _node)
    {
        if (selectedTower == 0) return;

        else if (selectedTower == 1)
        {
            turretToBuild = standardTurretPrefab;
        }
        else if (selectedTower == 5)
        {
            turretToBuild = resourceTurretPrefab;
        }

        GameObject turretToBuild2 = BuildManager.instance.GetTurretToBuild();
        thisTurret = (GameObject)Instantiate(turretToBuild2, _node.transform.position + turretOffset, _node.transform.rotation);
        thisTurret.transform.parent = _node.gameObject.transform;
        _node.turret = BuildManager.instance.thisTurret;
        _node.towerIndex = selectedTower;
    }

    public void RemoveAllTower()
    {
        foreach (Node _node in nodeList.nodes)
        {
            if (_node.turret != null)
            {
                Destroy(_node.turret);
            }
        }
    }

    public void RebuildLevel()
    {
        for (int i = 0; i < nodeList.Lv1.Count; i++)
        {
            if (nodeList.Lv1[i] == 0)
            {
                return;
            }
            else if (nodeList.Lv1[i] == 1)
            {
                WhichTowerToBuild(nodeList.Lv1[i], nodeList.nodes[i]);
            }
            else if (nodeList.Lv1[i] == 5)
            {
                WhichTowerToBuild(nodeList.Lv1[i], nodeList.nodes[i]);
            }
        }
    }
}
