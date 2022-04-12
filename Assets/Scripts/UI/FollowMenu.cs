using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMenu : MonoBehaviour
{
    public static FollowMenu instance;

    [Header("Utilities")]
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;
    public Vector3 originPosition;

    [Header("Nodes ref")]
    public Node SelectedNode;
    public Node lastNode;

    [Header("Colors")]
    public Color originalColor;
    public Color highlightColor;

    private Camera cam;

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
        originPosition = transform.position;

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(-400, -400, 0);
        
        if (FollowMenu.instance.SelectedNode == null)
        {
            pos = originPosition;
        }
        else if (FollowMenu.instance.SelectedNode != null)
        {
            pos = cam.WorldToScreenPoint(FollowMenu.instance.SelectedNode.transform.position + offset);
        }

        if (transform.position != pos)
            transform.position = pos;
    }

    public void AssignNode()
    {
        if (FollowMenu.instance.SelectedNode == null && FollowMenu.instance.lastNode == null)
        {
            //Debug.Log("No node assigned!");
        }
        else if (FollowMenu.instance.SelectedNode == null && FollowMenu.instance.lastNode != null)  // GameObject exist Behind UI
        {
            FollowMenu.instance.lastNode.rend.material.color = highlightColor;
        }
        else if (FollowMenu.instance.SelectedNode != null)
        {
            FollowMenu.instance.SelectedNode.rend.material.color = highlightColor;
        }
    }

    public void HideMenu()
    {
        if (FollowMenu.instance.SelectedNode == null && FollowMenu.instance.lastNode == null)
        {
            return;
        }

        transform.position = originPosition;
        FollowMenu.instance.SelectedNode.rend.material.color = originalColor;
        FollowMenu.instance.SelectedNode = lastNode = null;
    }

    public void UpgradeTower()
    {
        if (FollowMenu.instance.SelectedNode.TowerType != TowerType.NULL)
        {
            NodeManager.instance.UpgradeTower(FollowMenu.instance.SelectedNode);
            HideMenu();
        }
        else
        {
            //Debug.Log("No tower to upgrade here!");
        }
    }

    public void SellTower()
    {
        if (FollowMenu.instance.SelectedNode.TowerType != TowerType.NULL)
        {
            NodeManager.instance.SellTower(FollowMenu.instance.SelectedNode);
            HideMenu();
        }
        else
        {
            //Debug.Log("No tower to sell here!");
        }
    }

    public void BuildTowerWithType(int type)
    {
        TowerType towerType = (TowerType)type;
        //Debug.Log($"Build {towerType}");
        //print(towerType + " " + FollowMenu.instance.SelectedNode);

        if (FollowMenu.instance.SelectedNode != null)
        {
            NodeManager.instance.BuildTower(towerType, FollowMenu.instance.SelectedNode);
        }
        else
        {
            NodeManager.instance.BuildTower(towerType, FollowMenu.instance.lastNode);
        }
        HideMenu();
    }
}
