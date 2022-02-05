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
            turretToBuild = standardTurretPrefab;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("--Selected Tower 5--");
            turretToBuild = resourceTurretPrefab;
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

    public void BuildTower()
    {
        //switch (towerIndex)
        //{
        //    case 0:
        //        break;
        //    case 1:
        //        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        //        turret = (GameObject)Instantiate(turretToBuild, transform.position + turretOffset, transform.rotation);
        //        return;
        //    case 2:

        //    case 3:

        //    case 4:

        //    case 5:

        //    case 6:

        //    case 7:

        //    case 8:
        //}

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        GameObject turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
        towerIndex = 1;
        ResourceManager.Instance().ChangeCoin(-10);
    }

    public void BuildTower2(Node thisNode)
    {
        switch(buildTowerIndex)
        {
            case 1:

                break;
            case 5:

                break;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        thisTurret = (GameObject)Instantiate(turretToBuild, thisNode.transform.position + turretOffset, thisNode.transform.rotation);
        thisTurret.transform.parent = thisNode.gameObject.transform;
        thisNode.turret = BuildManager.instance.thisTurret;
        towerIndex = 1;
        ResourceManager.Instance().ChangeCoin(-10);
    }
}
