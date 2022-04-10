using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    
    [Header("Resource")]
    public TMP_Text coinText;
    public TMP_Text crystalText;
    public TMP_Text diamondText;

    public TMP_Text enemyText;
    public TMP_Text liveText;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one HUDManager in scene!");
        }
        
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText(ResourceManager.Instance().Coin);
        UpdateCrystalText(ResourceManager.Instance().Wood);
        UpdateDiamondText(ResourceManager.Instance().Crystal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void UpdateResourceText(ResourceType type)
    {
        Debug.Log($"Update {type}");
        switch (type)
        {
            case ResourceType.ALL:
            {
                coinText.text = ResourceManager.Instance().Coin.ToString();
                crystalText.text = ResourceManager.Instance().Wood.ToString();
                diamondText.text = ResourceManager.Instance().Crystal.ToString();
                break;
            }
            case ResourceType.COIN:
            {
                coinText.text = ResourceManager.Instance().Coin.ToString();
                Debug.Log($"coin {ResourceManager.Instance().Coin}");
                break;
            }
            case ResourceType.WOOD:
            {
                crystalText.text = ResourceManager.Instance().Wood.ToString();
                break;
            }
            case ResourceType.CRYSTAL:
            {
                diamondText.text = ResourceManager.Instance().Crystal.ToString();
                break;
            }
            default: break;
        }
    }

    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void UpdateCrystalText(int crystal)
    {
        crystalText.text = crystal.ToString();
    }

    public void UpdateDiamondText(int diamond)
    {
        diamondText.text = diamond.ToString();
    }

    public void UpdateLiveText(int live)
    {
        liveText.text = "Live " + live.ToString();
    }

    public void UpdateEnemyText(int enemy)
    {
        enemyText.text = "Enemy " + enemy.ToString();
    }
}
