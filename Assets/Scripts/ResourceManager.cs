using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceManager
{
    private static ResourceManager instance;
    
    private int coin;
    private int wood;
    private int rock;

    // private ResourceManager()
    // {
    //     coin = 0;
    //     wood = 0;
    //     rock = 0;
    // }

    public static ResourceManager Instance()
    {
        if (instance == null)
        {
            instance = new ResourceManager();
        }

        return instance;
    }

    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }

    public int Wood
    {
        get { return wood; }
        set { wood = value; }
    }

    public int Rock
    {
        get { return rock; }
        set { rock = value; }
    }

    public void ChangeCoin(int number)
    {
        coin += number;
    }

    private void ChangeWood(int number)
    {
        wood += number;
    }

    private void ChangeRock(int number)
    {
        rock += number;
    }
}
