using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateTowerUI : MonoBehaviour
{
    public GameObject listPanel;

    public static CreateTowerUI instance;

    public Node selectNode;

    private TowerType buttonEnums;

    public enumForUI button;



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

    //public void buildBasicTower()
    //{
    //    print(selectNode);
        
    //    NodeManager.instance.BuildTower(TowerType.ARCHER, selectNode);
    //    AudioManager.instance.Play(SoundType.SFX, "BowTowerBuild");
    //}


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

    public void AvailableTowerList()
    {
        print(selectNode);

        print(buttonEnums);

        switch (buttonEnums)
        {
            case TowerType.ARCHER:
                NodeManager.instance.BuildTower(TowerType.ARCHER, selectNode);
                break;
            case TowerType.FIRE:
                NodeManager.instance.BuildTower(TowerType.FIRE, selectNode);
                break;
        }


        //NodeManager.instance.BuildTower(TowerType.ARCHER, selectNode);
        AudioManager.instance.Play(SoundType.SFX, "BowTowerBuild");
    }

    
}

