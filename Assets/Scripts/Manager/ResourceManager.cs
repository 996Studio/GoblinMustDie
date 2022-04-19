using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceManager 
{
    private static ResourceManager instance;

    private int coin = 50;
    private int wood=0;
    private int crystal=0;

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

    public int Crystal
    {
        get { return crystal; }
        set { crystal = value; }
    }

    public void ChangeCoin(int number)
    {
        coin += number;
    }

    public void ChangeWood(int number)
    {
        wood += number;
    }

    public void ChangeCrystal(int number)
    {
        crystal += number;
    }
}
