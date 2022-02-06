using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("Resource")]
    public TMP_Text coinText;
    public TMP_Text crystalText;
    public TMP_Text diamondText;

    public TMP_Text enemyText;
    public TMP_Text liveText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText(ResourceManager.Instance().Coin);
        UpdateCrystalText(ResourceManager.Instance().Wood);
        UpdateDiamondText(ResourceManager.Instance().Rock);

        Resource.gatherResourceEvent += UpdateResourceText;
        SpawnPoint.updateEnemyNumEvent += UpdateEnemyText;
        MotherBase.updateLiveNumEvent += UpdateLiveText;
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
        switch (type)
        {
            case ResourceType.GOLD:
            {
                coinText.text = ResourceManager.Instance().Coin.ToString();
                break;
            }
            case ResourceType.WOOD:
            {
                crystalText.text = ResourceManager.Instance().Wood.ToString();
                break;
            }
            case ResourceType.ROCK:
            {
                diamondText.text = ResourceManager.Instance().Rock.ToString();
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
