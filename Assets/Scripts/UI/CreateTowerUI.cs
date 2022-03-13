using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class CreateTowerUI : MonoBehaviour
{
    
    public static CreateTowerUI instance;
    public Animator anim;
    
    public GameObject listPanel;
    public Node selectNode;
    public Node lastNode;
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
        //listPanel.SetActive(false);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // If click on UI, ignore ray cast hit on objects beyond
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // whatever tag you are looking for on your game object
                if (hit.collider.tag == "Node")
                {
                    return;
                }

                CreateTowerUI.instance.hidePanel();
            }
        }
    }

    public void showPanel()
    {
        //listPanel.SetActive(true);
        if (CreateTowerUI.instance.selectNode == null)
        {
            
        }
        else if (CreateTowerUI.instance.selectNode != null)
        {
            CreateTowerUI.instance.selectNode.rend.material.color = CreateTowerUI.instance.selectNode.originColor;
        }
        anim.SetBool("onShowPanel", true);
    }

    public void hidePanel()
    {
        if (CreateTowerUI.instance.selectNode == null)
        {
            return;
        }
        CreateTowerUI.instance.selectNode.isSelect = false;
        CreateTowerUI.instance.selectNode.rend.material.color = CreateTowerUI.instance.selectNode.originColor;
        CreateTowerUI.instance.selectNode = null;
        anim.SetBool("onShowPanel", false);
        //listPanel.SetActive(false);
    }
    
    public void upgradeTower()
    {
        print(selectNode);

        if (selectNode.TowerType != TowerType.NULL)
        {
            NodeManager.instance.UpgradeTower(selectNode);
            CreateTowerUI.instance.hidePanel();
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
            CreateTowerUI.instance.hidePanel();
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
        print(towerType + " " + CreateTowerUI.instance.selectNode);
        NodeManager.instance.BuildTower(towerType, CreateTowerUI.instance.selectNode);
        AudioManager.instance.Play(SoundType.SFX, "BowTowerBuild");
        CreateTowerUI.instance.hidePanel();
    }
}


