using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateTowerUI : MonoBehaviour
{
    public static event Action menuCall;

    public GameObject listPanel;

    public static CreateTowerUI instance;

    public Node selectNode;

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
        if (Input.GetMouseButtonDown(0))
        {
            menuCall?.Invoke();
            
        }
    }

    public void showPanel()
    {
        listPanel.SetActive(true);
    }

    public void hidePanel()
    {
        listPanel.SetActive(false);
    }

    public void buildBasicTower()
    {
        print(selectNode);
        
        NodeManager.instance.BuildTower(TowerType.ARCHER, selectNode);
        AudioManager.instance.Play(SoundType.SFX, "BowTowerBuild");
    }

    public void upgradeTower()
    {
        print(selectNode);

        NodeManager.instance.UpgradeTower(selectNode);
    }

    public void sellTower()
    {
        print(selectNode);

        NodeManager.instance.SellTower(selectNode, 1);

    }
}
