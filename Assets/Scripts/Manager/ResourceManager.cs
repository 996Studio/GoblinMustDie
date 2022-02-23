using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceManager 
{
    private static ResourceManager instance;

    private int coin = 10000;
    private int wood=10000;
    private int rock=10000;

    private ResourceManager()
    {
        
    }

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

    public void ChangeWood(int number)
    {
        wood += number;
    }

    public void ChangeRock(int number)
    {
        rock += number;
    }
}
