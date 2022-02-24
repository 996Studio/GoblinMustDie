using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateTowerUI : MonoBehaviour
{
    
    public static CreateTowerUI instance;
    
    public GameObject listPanel;
    public Node selectNode;
    //private TowerType buttonEnums;
    //public enumForUI button;
    //[SerializeField] ButtonToTower BuildThisType;


    private void Awake()
    {
        if (instance != null)
        {
            print("Hello???");
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        listPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showPanel()
    {
        listPanel.SetActive(true);
    }

    public void hidePanel()
    {
        listPanel.SetActive(false);
    }
    
    public void upgradeTower()
    {
        print(selectNode);

        if (selectNode.TowerType != TowerType.NULL)
        {
            NodeManager.instance.UpgradeTower(selectNode);
        }
        else
        {
            Debug.Log("No tower to upgrade here!");
        }
    }

    public void sellTower()
    {
        print(selectNode);

        if (selectNode.TowerType != TowerType.NULL)
        {
            NodeManager.instance.SellTower(selectNode);
        }
        else
        {
            Debug.Log("No tower to sell here!");
        }
    }

    public void BuildTower(int type)
    {
        TowerType towerType = (TowerType)type;
        Debug.Log($"Build {towerType}");
        NodeManager.instance.BuildTower(towerType, CreateTowerUI.instance.selectNode);
        AudioManager.instance.Play(SoundType.SFX, "BowTowerBuild");
    }
}

