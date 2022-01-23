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

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }


    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
