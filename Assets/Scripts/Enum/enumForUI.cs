using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ButtonToTower
{
    Archer=0, 
    Fire, 
    Water, 
    Ice,
    Thunder,
    Wooden,
    Crystal,
    Boost
}

public class enumForUI : MonoBehaviour
{
    //[SerializeField] ButtonToTower BuildThisType;
    [SerializeField] ButtonToTower BuildThisType;


    public void BuildThisTower()
    {
        switch (BuildThisType)
        {
            case ButtonToTower.Archer:
            {
                NodeManager.instance.BuildTower(TowerType.ARCHER, CreateTowerUI.instance.selectNode);
                break;
            }
            case ButtonToTower.Fire:
            {
                NodeManager.instance.BuildTower(TowerType.FIRE, CreateTowerUI.instance.selectNode);
                break;
            }
            case ButtonToTower.Ice:
            {
                NodeManager.instance.BuildTower(TowerType.ICE, CreateTowerUI.instance.selectNode);
                break;
            }

        }
    }
}

