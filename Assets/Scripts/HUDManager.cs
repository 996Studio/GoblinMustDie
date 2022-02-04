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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void UpdateCoin(int coin)
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
