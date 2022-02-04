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

    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;
    public NodeList nodeList;

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Debug.Log("SAVING");
        //    int[] data = nodeList.LoopThroughList();
        //    print("?");
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Debug.Log("LOADING");

        //}
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
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
        //towerIndex = 1;
        ResourceManager.Instance().ChangeCoin(-10);
    }
}
