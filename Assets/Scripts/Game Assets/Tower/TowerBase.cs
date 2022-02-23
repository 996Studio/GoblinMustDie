using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "tower",menuName = "Tower")]
public class TowerBase : ScriptableObject
{
    [SerializeField] private List<GameObject> towerPrefab;
    [SerializeField] private Vector3 towerBuildOffset;
    [SerializeField] private List<int> coinCost;
    [SerializeField] private List<int> woodCost;
    [SerializeField] private List<int> rockCost;
    [SerializeField] private List<int> coinGet;
    [SerializeField] private List<int> woodGet;
    [SerializeField] private List<int> rockGet;
    [SerializeField] private List<float> constructionTime;
    [SerializeField] private List<float> upgradeTime;
    
    [Header("Attack Tower")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<float> attackInterval;
    [SerializeField] private List<float> attackRange;
    [SerializeField] private List<int> attackValue;
    


    public List<GameObject> TowerPrefab
    {
        get { return towerPrefab; }
    }

    public Vector3 TowerBuildOffset
    {
        get { return towerBuildOffset; }
    }

    public List<int> CoinCost
    {
        get { return coinCost; }
    }

    public List<int> WoodCost
    {
        get { return woodCost; }
    }

    public List<int> RockCost
    {
        get { return rockCost; }
    }

    public List<int> CoinGet
    {
        get { return coinGet; }
    }

    public List<int> WoodGet
    {
        get { return woodGet; }
    }

    public List<int> RockGet
    {
        get { return rockGet; }
    }

    public List<float> AttackInterval
    {
        get { return attackInterval; }
    }

    public List<float> AttackRange
    {
        get { return attackRange; }
    }

    public List<int> AttackValue
    {
        get { return attackValue; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
