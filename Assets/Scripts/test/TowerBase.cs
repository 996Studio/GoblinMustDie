using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "tower",menuName = "Tower")]
public class TowerBase : ScriptableObject
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector3 towerBuildOffset;
    [SerializeField] private List<int> coinCost;
    [SerializeField] private List<int> woodCost;
    [SerializeField] private List<int> rockCost;
    [SerializeField] private List<int> coinGet;
    [SerializeField] private List<int> woodGet;
    [SerializeField] private List<int> rockGet;

    public GameObject TowerPrefab
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
